using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Persistence;
using EventStore.Serialization;
using Magnum.Extensions;
using Magnum.Policies;
using NLog;

namespace Documently.Infrastructure.Installers
{
	/// <summary>
	/// Installs Jonathan Oliver's Event Store with a JsonSerializer and an asynchronous dispatcher.
	/// </summary>
	public class EventStoreInstaller : IWindsorInstaller
	{
		static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly byte[] _encryptionKey = new byte[]
		{
			0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9,
			0xa, 0xb, 0xc, 0xd, 0xe, 0xf
		};

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<ExceptionPolicy>()
					.Named("eventstore")
					.LifestyleTransient()
					.Instance(
						ExceptionPolicy.InCaseOf<StorageUnavailableException>()
							.CircuitBreak(4.Seconds(), 5)),
				Component.For<IStoreEvents>()
					.UsingFactoryMethod(k =>
						{
							var policy = k.Resolve<ExceptionPolicy>("eventstore");
							var wup = Wireup.Init()
								.UsingAsynchronousDispatchScheduler(k.Resolve<IDispatchCommits>())
								.UsingRavenPersistence(Keys.RavenDbConnectionStringName)
									.ConsistentQueries()
									.PageEvery(int.MaxValue)
									.MaxServerPageSizeConfiguration(1024)
								.UsingCustomSerialization(BuildSerializer());
							while (true)
								try { return policy.Do(() => wup.Build()); }
								catch (StorageUnavailableException) { _logger.Error("Event Store unavailable, retrying with '{0}'", policy); }
						}));
		}

		// possibility to encrypt everything stored
		private ISerialize BuildSerializer()
		{
			var serializer = new JsonSerializer() as ISerialize;
			serializer = new GzipSerializer(serializer);
			return new RijndaelSerializer(serializer, _encryptionKey);
		}
	}
}
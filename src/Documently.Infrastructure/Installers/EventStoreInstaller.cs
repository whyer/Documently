using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Serialization;

namespace Documently.Infrastructure.Installers
{
	/// <summary>
	/// Installs Jonathan Oliver's Event Store with a JsonSerializer and synchronous dispatcher.
	/// </summary>
	public class EventStoreInstaller : IWindsorInstaller
	{
		private readonly byte[] _EncryptionKey = new byte[]
		{
			0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8, 0x9, 
			0xa, 0xb, 0xc, 0xd, 0xe, 0xf
		};

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			var eventStore = GetInitializedEventStore(container.Resolve<IPublishMessages>());
			var repository = new EventStoreRepository(eventStore,
				new AggregateFactory(),
				new ConflictDetector());

			container.Register(Component.For<IStoreEvents>().Instance(eventStore));
			container.Register(Component.For<IRepository>().Instance(repository));
		}

		private IStoreEvents GetInitializedEventStore(IPublishMessages bus)
		{
			return Wireup.Init()
				//.UsingRavenPersistence(BootStrapper.RavenDbConnectionStringName, new ByteStreamDocumentSerializer(BuildSerializer()))
				.UsingRavenPersistence(Keys.RavenDbConnectionStringName, 
					new ByteStreamDocumentSerializer(new JsonSerializer()))
				.UsingSynchronousDispatcher(bus)
				.Build();
		}

		// possibility to encrypt everything stored
		private ISerialize BuildSerializer()
		{
			var serializer = new JsonSerializer() as ISerialize;
			serializer = new GzipSerializer(serializer);
			return new RijndaelSerializer(serializer, _EncryptionKey);
		}
	}
}
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Domain.Service.Installers;
using Documently.Infrastructure;
using Documently.Infrastructure.Installers;
using MassTransit;
using NLog;
using Topshelf;
using log4net.Config;

namespace Documently.Domain.Service
{
	class Program
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private IWindsorContainer _container;
		private IServiceBus _bus;

		public static void Main(string[] args)
		{
			Thread.CurrentThread.Name = "Domain Service Main Thread";
			HostFactory.Run(x =>
			{
				x.Service<Program>(s =>
				{
					s.ConstructUsing(name => new Program());
					s.WhenStarted(p => p.Start());
					s.WhenStopped(p => p.Stop());
				});
				x.RunAsLocalSystem();

				x.SetDescription("Handles the domain logic for the Documently Application.");
				x.SetDisplayName("Documently Domain Service");
				x.SetServiceName("Documently.Domain.Service");
			});
		}

		private void Start()
		{
			BasicConfigurator.Configure(); // for TopShelf until it upgrades
			NLog.Config.SimpleConfigurator.ConfigureForConsoleLogging();

			_logger.Info("setting up domain service, installing components");

			_container = new WindsorContainer()
				.Install(
					new DomainInfraInstaller(),
					new RavenDbServerInstaller(),
					new CommandHandlerInstaller(),
					new EventStoreInstaller(),
					new BusInstaller(Keys.DomainServiceEndpoint)
					);

			_container.Register(Component.For<IWindsorContainer>().Instance(_container));
			_bus = _container.Resolve<IServiceBus>();

			_logger.Info("application configured, started running");
		}

		private void Stop()
		{
			_logger.Info("shutting down Domain Service");
			_container.Release(_bus);
			_container.Dispose();
		}
	}
}

using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Infrastructure;
using Documently.Infrastructure.Installers;
using MassTransit;
using Topshelf;
using log4net;
using log4net.Config;

namespace Documently.Domain.Service
{
	class Program
	{
		private static readonly ILog _Logger = LogManager.GetLogger(typeof (Program));
		
		private IWindsorContainer _Container;
		private IServiceBus _Bus;

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
			XmlConfigurator.Configure();
			_Logger.Info("setting up domain service, installing components");

			_Container = new WindsorContainer()
				.Install(
					new RavenDbServerInstaller(),
					new CommandHandlerInstaller(),
					new EventStoreInstaller(),
					new BusInstaller(Keys.DomainServiceEndpoint)
					);

			_Container.Register(Component.For<IWindsorContainer>().Instance(_Container));
			_Bus = _Container.Resolve<IServiceBus>();

			_Logger.Info("application configured, started running");
		}

		private void Stop()
		{
			_Logger.Info("shutting down Domain Service");
			_Container.Release(_Bus);
			_Container.Dispose();
		}
	}
}

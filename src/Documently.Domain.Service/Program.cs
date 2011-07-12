using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Infrastructure;
using Documently.Infrastructure.Installers;
using MassTransit;
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
			var p = new Program();
			try { 
				p.Start();
				Console.WriteLine("Started... Press a key to exit.");
				Console.ReadKey(true);
			}
			finally { p.Stop(); }
		}

		private void Start()
		{
			XmlConfigurator.Configure();
			_Logger.Info("setting up domain service, installing components");

			_Container = new WindsorContainer()
				.Install(
					new RavenDbServerInstaller(),
					new CommandHandlerInstaller(),
					new BusInstaller(Keys.DomainServiceEndpoint),
					new EventStoreInstaller());

			_Container.Register(Component.For<IWindsorContainer>().Instance(_Container));
			_Bus = _Container.Resolve<IServiceBus>();

			_Logger.Info("application configured, started running");
		}

		private void Stop()
		{
			_Container.Release(_Bus);
			_Container.Dispose();
		}
	}
}

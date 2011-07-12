using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Infrastructure;
using Documently.Infrastructure.Installers;
using MassTransit;

namespace Documently.Domain.Service
{
	class Program
	{
		private IWindsorContainer _Container;
		private IServiceBus _Bus;

		public static void Main(string[] args)
		{
			var p = new Program();
			try { p.Start(); }
			finally { p.Stop(); }
		}

		private void Start()
		{
			_Container = new WindsorContainer()
				.Install(
					new RavenDbServerInstaller(),
					new CommandHandlerInstaller(),
					new BusInstaller(Keys.DomainServiceEndpoint),
					new EventStoreInstaller());

			_Container.Register(Component.For<IWindsorContainer>().Instance(_Container));
			_Bus = _Container.Resolve<IServiceBus>();
		}

		private void Stop()
		{
			_Container.Release(_Bus);
			_Container.Dispose();
		}
	}
}

using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Documently.WpfClient.Infrastructure.Installers
{
	public class ViewModelInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<IWindowManager>()
					.ImplementedBy<WindowManager>()
					.LifeStyle.Singleton,
				
				Component.For<IEventAggregator>()
					.ImplementedBy<EventAggregator>()
					.LifeStyle.Singleton,
				
				AllTypes.FromThisAssembly().Pick());
		}
	}
}
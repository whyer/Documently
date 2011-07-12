using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Documently.Infrastructure.Installers
{
	public class RavenDbEmbeddedInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			// To use this: reference the embedded dlls.
			// run RavenDB InMemory
			//var store = new EmbeddableDocumentStore {RunInMemory = true};
		}
	}
}
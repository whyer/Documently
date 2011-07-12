using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Document;

namespace Documently.Infrastructure.Installers
{
	public class RavenDbServerInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			//use RavenDB Server as an event store and persists the read side (views) also to RavenDB Server
			IDocumentStore viewStore = new DocumentStore { ConnectionStringName = Keys.RavenDbConnectionStringName };
			viewStore.Initialize();

			container.Register(Component.For<IDocumentStore>().Instance(viewStore));
		}
	}
}
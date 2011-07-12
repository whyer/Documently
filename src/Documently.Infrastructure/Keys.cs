using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Infrastructure.Installers;
using Raven.Client;

namespace Documently.Infrastructure
{
	/// <summary>
	/// Helper class that creates the windsor container.
	/// </summary>
	public static class Keys
	{
		public static readonly string RavenDbConnectionStringName = "RavenDB";
		public static readonly string DomainServiceEndpoint = "rabbitmq://localhost/Documently.Domain.Service";

		public static IWindsorContainer BootStrap(IDocumentStore store, string endpointUri)
		{
			var container = new WindsorContainer();

			container.Register(Component.For<IDocumentStore>().Instance(store));

			// adds and configures all components using WindsorInstallers from executing assembly
			container.Install(
				);
			
			return container;
		}
	}
}
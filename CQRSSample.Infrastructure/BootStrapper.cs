using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CQRSSample.Domain.Events;
using CQRSSample.ReadModel;
using Raven.Client;

namespace CQRSSample.Infrastructure
{
	public class BootStrapper
	{
		public static readonly string RavenDbConnectionStringName = "RavenDB";

		public static IWindsorContainer BootStrap(IDocumentStore store)
		{
			var container = new WindsorContainer();

			container.Register(Component.For<IDocumentStore>().Instance(store));
			container.Register(Component.For<IWindsorContainer>().Instance(container));

			// adds and configures all components using WindsorInstallers from executing assembly  
			container.Install(FromAssembly.This());

			SetupDomainEventHandlers(container.Resolve<IBus>(), container.Resolve<IDocumentStore>());
			//RegisterEventHandlersInBus.BootStrap(container);

			return container;
		}

		private static void SetupDomainEventHandlers(IBus bus, IDocumentStore documentStore)
		{
			//TODO: Resolve through IoC

			var view = new CustomerListView(documentStore);
			bus.RegisterHandler<CustomerCreatedEvent>(view.Handle);
			bus.RegisterHandler<CustomerRelocatedEvent>(view.Handle);

			var addressView = new CustomerAddressView(documentStore);
			bus.RegisterHandler<CustomerCreatedEvent>(addressView.Handle);
			bus.RegisterHandler<CustomerRelocatedEvent>(addressView.Handle);
		}
	}
}
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CQRSSample.ReadModel;
using Documently.Domain.Events;
using MassTransit;
using Raven.Client;

namespace Documently.Infrastructure
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

			var view = SetupDomainEventHandlers(container.Resolve<IServiceBus>(), container.Resolve<IDocumentStore>());
			//RegisterEventHandlersInBus.BootStrap(container);

			container.Register(Component.For<CustomerListView>().Instance(view));

			return container;
		}

		private static CustomerListView SetupDomainEventHandlers(IServiceBus bus, IDocumentStore documentStore)
		{
			//TODO: Resolve through IoC

			var view = new CustomerListView(documentStore);
			bus.SubscribeHandler<CustomerCreatedEvent>(view.Handle);
			bus.SubscribeHandler<CustomerRelocatedEvent>(view.Handle);

			var addressView = new CustomerAddressView(documentStore);
			bus.SubscribeHandler<CustomerCreatedEvent>(addressView.Handle);
			bus.SubscribeHandler<CustomerRelocatedEvent>(addressView.Handle);

			return view;
		}
	}
}
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Documently.Domain.Events;
using Documently.ReadModel;
using MassTransit;
using Raven.Client;

namespace Documently.Infrastructure
{
	/// <summary>
	/// Helper class that creates the windsor container.
	/// </summary>
	public static class BootStrapper
	{
		public static readonly string RavenDbConnectionStringName = "RavenDB";

		public static IWindsorContainer BootStrap(IDocumentStore store)
		{
			var container = new WindsorContainer();

			container.Register(Component.For<IDocumentStore>().Instance(store));
			container.Register(Component.For<IWindsorContainer>().Instance(container));

			// adds and configures all components using WindsorInstallers from executing assembly
			container.Install(FromAssembly.This());

			//var view = SetupDomainEventHandlers(
			//    container.Resolve<IServiceBus>(), 
			//    container.Resolve<IDocumentStore>());

			RegisterEventHandlersInBus.BootStrap(container);

			//container.Register(Component.For<CustomerListView>().Instance(view));

			return container;
		}

		private static CustomerListView SetupDomainEventHandlers(IServiceBus bus, IDocumentStore documentStore)
		{
			//TODO: Resolve through IoC

			var view = new CustomerListView(documentStore);
			bus.SubscribeHandler<CustomerCreatedEvent>(view.Consume);
			bus.SubscribeHandler<CustomerRelocatedEvent>(view.Consume);

			var addressView = new CustomerAddressView(documentStore);
			bus.SubscribeHandler<CustomerCreatedEvent>(addressView.Consume);
			bus.SubscribeHandler<CustomerRelocatedEvent>(addressView.Consume);

			return view;
		}
	}
}
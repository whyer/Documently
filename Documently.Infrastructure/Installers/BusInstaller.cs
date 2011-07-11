using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;

namespace Documently.Infrastructure.Installers
{
	/// <summary>
	/// Installs the service bus into the container.
	/// </summary>
	public class BusInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			//Bus
#if DEBUG
			var bus = new InProcessBus(container);
			container.Register(Component.For<IBus>().Instance(bus));
#else
			var bus = ServiceBusFactory.New(sbc =>
			{
				sbc.UseRabbitMq();
				sbc.ReceiveFrom("rabbitmq://localhost/Documently");
				sbc.UseRabbitMqRouting();
				sbc.Subscribe(s => s.LoadFrom(container));
			});

			var busAdapter = new MassTransitPublisher(bus);

			container.Register(Component.For<IServiceBus>().Instance(bus),
				Component.For<IBus>().Instance(busAdapter));
#endif
		}
	}
}
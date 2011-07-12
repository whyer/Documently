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
		private readonly string _EndpointUri;

		public BusInstaller(string endpointUri)
		{
			_EndpointUri = endpointUri;
		}

		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			// in proc bus
			//var bus = new InProcessBus(container);
			//container.Register(Component.For<IBus>().Instance(bus));

			// masstransit bus
			var bus = ServiceBusFactory.New(sbc =>
			{
				sbc.UseRabbitMq();
				sbc.ReceiveFrom(_EndpointUri);
				sbc.UseRabbitMqRouting();
				sbc.Subscribe(s => s.LoadFrom(container));
			});

			var busAdapter = new MassTransitPublisher(bus);

			container.Register(
				Component.For<IServiceBus>().Instance(bus),
				Component.For<IBus>().Instance(busAdapter));
		}
	}
}
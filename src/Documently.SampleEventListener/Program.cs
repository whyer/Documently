using System;
using Documently.Messages;
using Documently.Messages.CustomerEvents;
using Magnum.Extensions;		
using MassTransit;

namespace Documently.SampleEventListener
{
	class Program
	{
		static void Main(string[] args)
		{
			var bus = ServiceBusFactory.New(cfg =>
				{
					cfg.ReceiveFrom("rabbitmq://localhost/Documently.SampleEventListener");
					cfg.UseRabbitMqRouting();
					cfg.Subscribe(s =>
						s.Handler<Created>(
							created => Console.WriteLine("Customer Created: {0}".FormatWith(created)))
							.Transient());
				});

			Console.ReadKey(true);

			bus.Dispose();
		}
	}
}

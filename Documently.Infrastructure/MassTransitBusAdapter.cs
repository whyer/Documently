using System;
using EventStore;
using EventStore.Dispatcher;
using MassTransit;

namespace Documently.Infrastructure
{
	internal class MassTransitBusAdapter : IBus, IPublishMessages
	{
		private readonly IServiceBus _Bus;

		public MassTransitBusAdapter(IServiceBus bus)
		{
			_Bus = bus;
		}

		void IBus.Send<T>(T command)
		{
			_Bus.Publish(command);
		}

		void IBus.RegisterHandler<T>(Action<T> handler)
		{
			_Bus.SubscribeHandler(handler);
		}

		public void Dispose()
		{
			_Bus.Dispose();
		}

		void IPublishMessages.Publish(Commit commit)
		{
			_Bus.Publish(commit);
		}
	}
}
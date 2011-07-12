using System;
using EventStore;
using EventStore.Dispatcher;
using MassTransit;
using Magnum.Reflection;

namespace Documently.Infrastructure
{
	public class MassTransitPublisher : IBus, IPublishMessages
	{
		private readonly IServiceBus _Bus;

		public MassTransitPublisher(IServiceBus bus)
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

		void IPublishMessages.Publish(Commit commit)
		{
			commit.Events.ForEach(@event =>
				{
					this.FastInvoke("PublishEvent", @event.Body);
				});
		}

		void PublishEvent<T>(T message)
			where T : class
		{
			_Bus.Publish(message);
		}

		public void Dispose()
		{
			_Bus.Dispose();
		}
	}
}
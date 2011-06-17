using System;
using Documently.Commands;
using Documently.Domain.Events;
using EventStore;
using EventStore.Dispatcher;
using MassTransit;

namespace Documently.Infrastructure
{
	class MassTransitBusAdapter : IBus, IPublishMessages
	{
		private readonly IServiceBus _Bus;

		public MassTransitBusAdapter(IServiceBus bus)
		{
			_Bus = bus;
		}

		public void Send<T>(T command) where T : Command
		{
			_Bus.Publish(command);
		}

		public void RegisterHandler<T>(Action<T> handler) where T : DomainEvent
		{
			_Bus.SubscribeHandler(handler);
		}

		public void Dispose()
		{
			_Bus.Dispose();
		}

		public void Publish(Commit commit)
		{
			_Bus.Publish(commit);
		}
	}
}
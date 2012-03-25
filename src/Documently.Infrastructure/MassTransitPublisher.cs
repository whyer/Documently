using System;
using Documently.Messages;
using EventStore;
using EventStore.Dispatcher;
using MassTransit;
using Magnum.Reflection;
using MassTransit.Util;

namespace Documently.Infrastructure
{
	public class MassTransitPublisher : IBus, IDispatchCommits
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

		void IDispatchCommits.Dispatch(Commit commit)
		{
			commit.Events.ForEach(evt => this.FastInvoke("PublishEvent", evt.Body));
		}

		[UsedImplicitly]
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
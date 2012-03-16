using System;
using System.Collections.Generic;
using Castle.Windsor;
using Documently.Commands;
using Documently.Domain.CommandHandlers;
using Documently.Infrastructure.Misc;
using Documently.Messages;
using EventStore;
using EventStore.Dispatcher;
using MassTransit;
using MassTransit.Diagnostics.Introspection;
using MassTransit.Pipeline;

namespace Documently.Infrastructure
{
	public class InProcessBus : IBus, IDispatchCommits, IServiceBus
	{
		private readonly IWindsorContainer _Container;

		private readonly Dictionary<Type, List<Action<DomainEvent>>> _Routes =
			new Dictionary<Type, List<Action<DomainEvent>>>();

		public InProcessBus(IWindsorContainer container)
		{
			_Container = container;
		}

		void IServiceBus.Publish<T>(T message, Action<IPublishContext<T>> contextCallback)
		{
			throw new NotImplementedException(); // Ignore?
		}

		void IBus.Send<T>(T command)
		{
			if (command == null) throw new ArgumentNullException("command");
			GetCommandHandlerForCommand<T>()
				.Consume(command);
		}

		private Consumes<T>.All GetCommandHandlerForCommand<T>() where T : class, Command
		{
			return _Container.Resolve<Consumes<T>.All>();
		}

		void IBus.RegisterHandler<T>(Action<T> handler)
		{
			List<Action<DomainEvent>> handlers;
			if (!_Routes.TryGetValue(typeof (T), out handlers))
			{
				handlers = new List<Action<DomainEvent>>();
				_Routes.Add(typeof (T), handlers);
			}
			handlers.Add(DelegateAdjuster.CastArgument<DomainEvent, T>(x => handler(x)));
		}

		void IDispatchCommits.Dispatch(Commit commit)
		{
			foreach (var @event in commit.Events)
			{
				List<Action<DomainEvent>> handlers;

				if (!_Routes.TryGetValue(@event.Body.GetType(), out handlers)) 
					return;

				foreach (var handler in handlers)
				{
					//dispatch on thread pool for added awesomeness
					//var handler1 = handler;
					//ThreadPool.QueueUserWorkItem(x => handler1(@event));
					handler((DomainEvent) @event.Body);
				}
			}
		}

		IEndpoint IServiceBus.GetEndpoint(Uri address)
		{
			throw new NotImplementedException();
		}

		UnsubscribeAction IServiceBus.Configure(Func<IInboundPipelineConfigurator, UnsubscribeAction> configure)
		{
			throw new NotSupportedException();
		}

		IEndpoint IServiceBus.Endpoint
		{
			get { throw new NotSupportedException(); }
		}

		IInboundMessagePipeline IServiceBus.InboundPipeline
		{
			get { throw new NotSupportedException(); }
		}

		IOutboundMessagePipeline IServiceBus.OutboundPipeline
		{
			get { throw new NotSupportedException(); }
		}

		IServiceBus IServiceBus.ControlBus
		{
			get { throw new NotSupportedException(); }
		}

		IEndpointCache IServiceBus.EndpointCache
		{
			get { throw new NotSupportedException(); }
		}

		public void Dispose()
		{
		}

		public void Inspect(DiagnosticsProbe probe)
		{
			probe.Add("mt.bus", "in-memory");
		}
	}
}
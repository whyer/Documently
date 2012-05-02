using System;
using System.Collections.Generic;
using Documently.Messages;
using MassTransit.Util;

namespace Documently.Domain
{
	public class EventRouter
	{
		private readonly dynamic _instance;

		private readonly List<DomainEvent> _raisedEvents = new List<DomainEvent>();

		private EventRouter([NotNull] AggregateRoot instance)
		{
			if (instance == null) throw new ArgumentNullException("instance");
		    _instance = PrivateReflectionDynamicObject.WrapObjectIfNeeded(instance);
		}

		public void ApplyEvent<T>(T evt) where T : DomainEvent
		{
			_instance.Apply(evt);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="e"></param>
		/// <exception cref="ArgumentException">e.Version != instance.Version + 1</exception>
		public void RaiseEvent<T>(T e) where T : DomainEvent
		{
			if (e.Version != _instance.Version + 1)
				throw new ArgumentException("The event needs to increment the aggregate's version by one.");
			
			_raisedEvents.Add(e);
		}

		[NotNull]
		public IEnumerable<DomainEvent> GetUncommitted()
		{
			return new List<DomainEvent>(_raisedEvents);
		}

		public static EventRouter For<T>(T instance)
			where T : AggregateRoot
		{
			return new EventRouter(instance);
		}
	}
}
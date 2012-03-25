// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System;
using System.Collections.Generic;
using Documently.Messages;
using MassTransit;
using MassTransit.Util;

namespace Documently.Domain
{
	public interface AggregateRoot
	{
		/// <summary>
		/// Gets the id of the aggregate root
		/// </summary>
		NewId Id { get; }

		/// <summary>
		/// Gets the current aggregate root version. This correspond to the event sequence number.
		/// </summary>
		uint Version { get; }
	}

	public interface EventAccessor
	{
		EventRouter Events { get; }
	}

	public class EventRouter
	{
		private readonly AggregateRoot _instance;

		private readonly List<DomainEvent> _raisedEvents = new List<DomainEvent>();

		private EventRouter([NotNull] AggregateRoot instance)
		{
			if (instance == null) throw new ArgumentNullException("instance");
			_instance = instance;
		}

		public void ApplyEvent<T>(T evt) where T : DomainEvent
		{
			dynamic i = _instance;
			i.Apply(evt);
		}

		public void RaiseEvent<T>(T e) where T : DomainEvent
		{
			_raisedEvents.Add(e);
		}

		public static EventRouter For<T>(T instance)
			where T : AggregateRoot
		{
			return new EventRouter(instance);
		}
	}
}
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
using Documently.Domain.CommandHandlers.ForCustomer;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Messages;
using Documently.Messages.CustCommands;
using Documently.Messages.CustEvents;
using FakeItEasy;
using Machine.Specifications;
using MassTransit;
using I = Magnum.Reflection.InterfaceImplementationExtensions;
using Magnum.Extensions;
using System.Linq;

// ReSharper disable InconsistentNaming
// ReSharper disable UnassignedField.Local

namespace Documently.Domain.CommandHandlers.Tests
{
	public class Handler_and_Aggregate_spec
	{
		protected static DomainRepository repo;
		protected static List<DomainEvent> yieldedEvents = new List<DomainEvent>();

		Establish context = () =>
			{
				var r = A.Fake<DomainRepository>();

				A.CallTo(() => r.Save(A<Customer>.Ignored, A<NewId>.Ignored, A<IDictionary<string, string>>.Ignored))
					.Invokes(fake => yieldedEvents.AddRange(((EventAccessor) fake.Arguments[0]).Events.GetUncommitted()));

				repo = r;
			};

		protected static IConsumeContext<T> a_command<T>(T command)
			where T : class
		{
			var consumeContext = A.Fake<IConsumeContext<T>>();
			A.CallTo(() => consumeContext.MessageId).Returns(NewId.Next().ToString());
			A.CallTo(() => consumeContext.Message).Returns(command);
			return consumeContext;
		}
	}

	[Behaviors]
	public class Event_versions_are_greater_than_zero
	{
		protected static IEnumerable<DomainEvent> yieldedEvents;

		It should_specify_correct_event_versions = () => 
			yieldedEvents
				.ToList()
				.ForEach(e => e.Version.ShouldBeGreaterThan(0U));
	}

	[Behaviors]
	public class Event_versions_are_monotonically_increasing
	{
		protected static IEnumerable<DomainEvent> yieldedEvents;

		It should_contain_only_events_with_increasing_versions = () => 
			yieldedEvents.OrderBy(x => x.Version)
				.Zip(yieldedEvents.Skip(1).OrderBy(x => x.Version), Tuple.Create)
				.ToList()
				.ForEach(t => t.Item1.Version.ShouldEqual(t.Item2.Version - 1U));
	}

	[Behaviors]
	public class Events_has_non_default_aggregate_root_id
	{
		protected static IEnumerable<DomainEvent> yieldedEvents;

		It should_have_non_default_ar_ids = () =>
			yieldedEvents
				.ToList()
				.ForEach(e => e.AggregateId.ShouldNotEqual(NewId.Empty));
	}

	[Subject(typeof (Customer))]
	public class when_registering_new
		: Handler_and_Aggregate_spec
	{
		static RegisterNewHandler handler;

		Establish context = () =>
			handler = new RegisterNewHandler(() => repo);

		Because of = () =>
			handler.Consume(a_command<RegisterNew>(new MsgImpl.RegisterNew
			{
				AggregateId = NewId.Next(),
				CustomerName = "Henrik F",
				PhoneNumber = "+46727344868",
				Address = new MsgImpl.Address
					{
						Street = "Drottninggatan",
						StreetNumber = 108,
						PostalCode = "113 60",
						City = "Stockholm"
					}
			}));

		It should_yield_customer_registered = () => yieldedEvents.ShouldContain<Registered>();

		Behaves_like<Event_versions_are_greater_than_zero> should_specify_versions_above_zero;
		Behaves_like<Event_versions_are_monotonically_increasing> should_specify_monotonically_increasing_versions;
		Behaves_like<Events_has_non_default_aggregate_root_id> should_have_non_default_ar_ids;
	}
}
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

using System.Collections.Generic;
using Documently.Domain.CommandHandlers.ForCustomer;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Domain.CommandHandlers.Tests.Behaviors;
using Documently.Messages;
using Documently.Messages.CustCommands;
using Documently.Messages.CustEvents;
using FakeItEasy;
using Machine.Specifications;
using Magnum.Reflection;
using MassTransit;
using System.Linq;

// ReSharper disable InconsistentNaming
// ReSharper disable UnassignedField.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Documently.Domain.CommandHandlers.Tests
{
	public class Handler_and_Aggregate_spec
	{
		protected static DomainRepository repo;
		protected static List<DomainEvent> yieldedEvents = new List<DomainEvent>();

		interface AnyAr : AggregateRoot, EventAccessor {}

		Establish context = () =>
			{
				repo = A.Fake<DomainRepository>();
			};

		protected static void has_seen_events<TAr>(params DomainEvent[] evtSeen)
			where TAr : class, EventAccessor, AggregateRoot
		{
			if (repo == null) throw new SpecificationException("call setup_repository_for<Customer>(); before has_seen_events");
			var ar = (EventAccessor)FastActivator.Create(typeof(TAr));
			evtSeen.ToList().ForEach(ar.Events.ApplyEvent);
			var aggregateId = evtSeen.First().AggregateId;
			var maxVersion = evtSeen.Max(e => e.Version);
			A.CallTo(() => repo.GetById<TAr>(aggregateId, maxVersion)).Returns(ar as TAr);
		}

		protected static IConsumeContext<T> a_command<T>(T command)
			where T : class
		{
			var consumeContext = A.Fake<IConsumeContext<T>>();
			A.CallTo(() => consumeContext.MessageId).Returns(NewId.Next().ToString());
			A.CallTo(() => consumeContext.Message).Returns(command);
			return consumeContext;
		}

		protected static void setup_repository_for<TAr>()
			where TAr : class, AggregateRoot, EventAccessor
		{
			A.CallTo(() => repo.Save(A<TAr>.Ignored, A<NewId>.Ignored, A<IDictionary<string, string>>.Ignored))
					.WithAnyArguments()
					.Invokes(fake => yieldedEvents.AddRange(((EventAccessor)fake.Arguments[0]).Events.GetUncommitted()));
		}
	}

	[Subject(typeof (Customer))]
	public class when_registering_new
		: Handler_and_Aggregate_spec
	{
		static RegisterNewHandler handler;

		Establish context = () =>
			{
				setup_repository_for<Customer>();
				handler = new RegisterNewHandler(() => repo);
			};

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
		It should_specify_correct_number = () => yieldedEvents.ShouldContain<Registered>(r => 
			r.PhoneNumber.ShouldEqual("+46727344868"));

		Behaves_like<Event_versions_are_greater_than_zero> should_specify_versions_above_zero;
		Behaves_like<Event_versions_are_monotonically_increasing> should_specify_monotonically_increasing_versions;
		Behaves_like<Events_has_non_default_aggregate_root_id> should_have_non_default_ar_ids;
	}

	[Subject(typeof(Customer))]
	public class When_customer_relocates
		: Handler_and_Aggregate_spec
	{
		static NewId AggregateId = NewId.Next();

		static RelocateTheCustomerHandler handler;

		Establish context = () =>
			{
				setup_repository_for<Customer>();
				has_seen_events<Customer>(CustomerTestFactory.Registered(AggregateId));
				handler = new RelocateTheCustomerHandler(() => repo);
			};

		Because of = () =>
			handler.Consume(a_command<RelocateTheCustomer>(new MsgImpl.Relocate
				{
					AggregateId = AggregateId,
					NewAddress = new MsgImpl.Address
						{
							City = "Berlin",
							PostalCode = "4566",
							Street = "FünfteStrasse",
							StreetNumber = 45
						},
					Version = 1U
				}));

		It should_have_loaded_existing = () =>
			A.CallTo(() => repo.GetById<Customer>(AggregateId, 1)).MustHaveHappened(Repeated.Exactly.Once);

		It should_have_published_relocated_event = () => 
			yieldedEvents.ShouldContain<Relocated>(
				r => r.City.ShouldEqual("Berlin"));

		Behaves_like<Event_versions_are_greater_than_zero> should_specify_versions_above_zero;
		Behaves_like<Event_versions_are_monotonically_increasing> should_specify_monotonically_increasing_versions;
		Behaves_like<Events_has_non_default_aggregate_root_id> should_have_non_default_ar_ids;
	}
}
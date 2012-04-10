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
using Castle.Core;
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

// ReSharper disable InconsistentNaming

namespace Documently.Domain.CommandHandlers.Tests
{
	static class CustomerFac
	{
	}

	[Behaviors]
	public class Event_versions_are_greater_than_zero
	{
		static IEnumerable<DomainEvent> yieldedEvents;

		It should_specify_correct_event_versions =
			() => yieldedEvents.ForEach(
				e => e.Version.ShouldBeGreaterThan(0));
	}

	public class Yields_events_context
	{
		protected static IEnumerable<DomainEvent> yieldedEvents;
	}

	[Subject(typeof (Customer))]
	public class when_registering_new
		: Yields_events_context
	{
		static IConsumeContext<RegisterNew> consumeContext;
		static RegisterNewHandler handler;

		Establish context = () =>
			{
				var repo = A.Fake<DomainRepository>();

				A.CallTo(() => repo.Save(A<Customer>.Ignored, A.Dummy<NewId>(), null))
					.WithAnyArguments()
					.Invokes(fake =>
						{
							var savedAr = fake.Arguments[0] as EventAccessor;
							savedAr.ShouldNotBeNull();
							yieldedEvents = savedAr.Events.GetUncommitted();
						});

				handler = new RegisterNewHandler(() => repo);
				consumeContext = A.Fake<IConsumeContext<RegisterNew>>();

				A.CallTo(() => consumeContext.MessageId)
					.Returns(NewId.Next().ToString());

				var msg = new MsgImpl.RegisterNew
					{
						CustomerName = "Henrik F",
						PhoneNumber = "+46788888888",
						Address = new MsgImpl.Address
							{
								Street = "Drottninggatan 108",
								StreetNumber = 56,
								PostalCode = "",
								City = ""
							}
					};

				A.CallTo(() => consumeContext.Message).Returns(msg);
			};

		Because of =
			() => handler.Consume(consumeContext);

		It should_yield_customer_registered =
			() => yieldedEvents.ShouldContain(e => e.Implements<Registered>());

		Behaves_like<Event_versions_are_greater_than_zero> should_specify_correct_versions;
	}
}
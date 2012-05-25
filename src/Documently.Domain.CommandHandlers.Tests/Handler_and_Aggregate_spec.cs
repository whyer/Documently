using System;
using System.Collections.Generic;
using System.Linq;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Messages;
using FakeItEasy;
using Machine.Specifications;
using Magnum;
using Magnum.Reflection;
using MassTransit;

namespace Documently.Domain.CommandHandlers.Tests
{
	public abstract class Handler_and_Aggregate_spec
	{
		// just read the method names of this class when starting out,
		// the actual specs are further down.

		protected static DomainRepository repo;
		protected static List<DomainEvent> yieldedEvents = new List<DomainEvent>();

		Establish context = () =>
		                    	{
		                    		repo = A.Fake<DomainRepository>();
		                    	};

		protected static void has_seen_events<TAr>(params DomainEvent[] evtSeen)
			where TAr : class, EventAccessor, AggregateRoot
		{
			if (repo == null) throw new SpecificationException("call setup_repository_for<MyAr>(); before has_seen_events");
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
			A.CallTo(() => consumeContext.MessageId).Returns(CombGuid.Generate().ToString());
			A.CallTo(() => consumeContext.Message).Returns(command);
			return consumeContext;
		}

		protected static void setup_repository_for<TAr>()
			where TAr : class, AggregateRoot, EventAccessor
		{
			A.CallTo(() => repo.Save(A<TAr>.Ignored, A<Guid>.Ignored, A<IDictionary<string, string>>.Ignored))
				.WithAnyArguments()
				.Invokes(fake =>
				         	{
				         		yieldedEvents.Clear();
				         		yieldedEvents.AddRange(
				         			((EventAccessor)fake.Arguments[0]).Events.GetUncommitted());
				         	});
		}
	}
}
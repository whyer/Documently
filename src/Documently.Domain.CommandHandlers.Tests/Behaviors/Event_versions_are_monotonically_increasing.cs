using System;
using System.Collections.Generic;
using System.Linq;
using Documently.Messages;
using Machine.Specifications;

namespace Documently.Domain.CommandHandlers.Tests.Behaviors
{
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
}
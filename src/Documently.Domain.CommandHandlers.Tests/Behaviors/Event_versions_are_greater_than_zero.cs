using System.Collections.Generic;
using System.Linq;
using Documently.Messages;
using Machine.Specifications;

namespace Documently.Domain.CommandHandlers.Tests.Behaviors
{
	[Behaviors]
	public class Event_versions_are_greater_than_zero
	{
		protected static IEnumerable<DomainEvent> yieldedEvents;

		It should_specify_correct_event_versions = () => 
		                                           yieldedEvents
		                                           	.ToList()
		                                           	.ForEach(e => e.Version.ShouldBeGreaterThan(0U));
	}
}
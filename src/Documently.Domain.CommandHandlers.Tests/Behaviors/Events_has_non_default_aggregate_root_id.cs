using System.Collections.Generic;
using System.Linq;
using Documently.Messages;
using Machine.Specifications;
using MassTransit;

namespace Documently.Domain.CommandHandlers.Tests.Behaviors
{
	[Behaviors]
	public class Events_has_non_default_aggregate_root_id
	{
		protected static IEnumerable<DomainEvent> yieldedEvents;

		It should_have_non_default_ar_ids = () =>
		                                    yieldedEvents
		                                    	.ToList()
		                                    	.ForEach(e => e.AggregateId.ShouldNotEqual(NewId.Empty));
	}
}
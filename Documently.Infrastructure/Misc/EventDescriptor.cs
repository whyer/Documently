using System;
using Documently.Domain.Events;

namespace Documently.Infrastructure
{
	public class EventDescriptor
	{
		public readonly DomainEvent EventData;
		public readonly Guid Id;
		public readonly int Version;

		public EventDescriptor(Guid aggregateId, DomainEvent eventData, int version)
		{
			EventData = eventData;
			Version = version;
			Id = aggregateId;
		}
	}
}
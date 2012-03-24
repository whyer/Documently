using System;
using Documently.Messages;

namespace Documently.Infrastructure.Misc
{
	public class EventDescriptor
	{
		public readonly DomainEvent EventData;
		public readonly NewId Id;
		public readonly int Version;

		public EventDescriptor(NewId aggregateId, DomainEvent eventData, int version)
		{
			EventData = eventData;
			Version = version;
			Id = aggregateId;
		}
	}
}
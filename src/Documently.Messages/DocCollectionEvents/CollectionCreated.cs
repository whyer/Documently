using System;

namespace Documently.Messages.DocumentCollection
{
	[Serializable]
	public class CollectionCreated : DomainEvent
	{
		public CollectionCreated(Guid aggregateId, string name)
		{
			Name = name;
			AggregateId = aggregateId;
		}

		public Guid AggregateId { get; set; }
		public int Version { get; set; }
		public string Name { get; set; }
	}
}
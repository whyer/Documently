using System;

namespace Documently.Messages.DocumentMetaData
{
	public interface AssociatedWithCollection : DomainEvent
	{
		public Guid CollectionId { get; protected set; }
		public Guid AggregateId { get; private set; }
		public int Version { get; set; }
	}
}
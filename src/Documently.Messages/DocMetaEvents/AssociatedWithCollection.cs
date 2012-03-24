using System;

namespace Documently.Messages.DocumentMetaData
{
	public interface AssociatedWithCollection : DomainEvent
	{
		public NewId CollectionId { get; protected set; }
		public NewId AggregateId { get; private set; }
		public int Version { get; set; }
	}
}
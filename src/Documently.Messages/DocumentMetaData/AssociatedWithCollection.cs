using System;

namespace Documently.Messages.DocumentMetaData
{
	[Serializable]
	public class AssociatedWithCollection : DomainEvent
	{
		public AssociatedWithCollection(Guid documentId, Guid collectionId, int version)
		{
			AggregateId = documentId;
			CollectionId = collectionId;
			Version = version;
		}

		public Guid CollectionId { get; protected set; }
		public Guid AggregateId { get; private set; }
		public int Version { get; set; }
	}
}
using System;

namespace Documently.Commands.DocumentMetaData
{
	[Serializable]
	public class AssociateWithCollection : Command
	{
		public Guid CollectionId { get; protected set; }

		public AssociateWithCollection(Guid docId, Guid collectionId)
		{
			AggregateId = docId;
			CollectionId = collectionId;
		}

		public Guid AggregateId { get; set; }
		public int Version { get; set; }
	}
}
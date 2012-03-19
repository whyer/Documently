using System;

namespace Documently.Commands
{
	[Serializable]
	public class InitializeDocumentIndexing : Command
	{
		public InitializeDocumentIndexing(Guid aggregateId, Guid blobId)
		{
			AggregateId = aggregateId;
			BlobId = blobId;
		}

		public Guid BlobId { get; protected set; }
		public Guid AggregateId { get; set; }
		public int Version { get; set; }
	}
}
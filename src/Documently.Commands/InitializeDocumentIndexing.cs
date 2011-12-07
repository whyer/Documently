using System;

namespace Documently.Commands
{
	[Serializable]
	public class InitializeDocumentIndexing : Command
	{
		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected InitializeDocumentIndexing()
		{
		}

		public InitializeDocumentIndexing(Guid aggregateId, Guid blobId) : base(aggregateId)
		{
			BlobId = blobId;
		}

		public Guid BlobId { get; protected set; }
	}
}
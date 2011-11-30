using System;

namespace Documently.Commands
{
	[Serializable]
	public class InitializeDocumentIndexing : Command
	{
		private Guid _BlobId;

		public InitializeDocumentIndexing()
		{
		}

		public InitializeDocumentIndexing(Guid aggregateId, Guid blobId) : base(aggregateId)
		{
			_BlobId = blobId;
		}

		public Guid BlobId
		{
			get { return _BlobId; }
            set { _BlobId = value; }
		}
	}
}
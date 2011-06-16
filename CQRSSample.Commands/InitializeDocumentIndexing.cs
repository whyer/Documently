using System;

namespace Documently.Commands
{
	public class InitializeDocumentIndexing : Command
	{
		private readonly Guid _BlobId;

		public InitializeDocumentIndexing(Guid aggregateId, Guid blobId) : base(aggregateId)
		{
			_BlobId = blobId;
		}

		public Guid BlobId
		{
			get { return _BlobId; }
		}
	}
}
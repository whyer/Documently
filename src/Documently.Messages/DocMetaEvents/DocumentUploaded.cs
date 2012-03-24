using System;

namespace Documently.Messages.DocumentMetaData
{
	/// <summary>
	/// When the file finishes uploading from disk or network, this event is raised.
	/// </summary>
	[Serializable]
	public class DocumentUploaded : DomainEvent
	{
		public DocumentUploaded(NewId blobId, NewId arId, int version)
		{
			BlobId = blobId;
			AggregateId = arId;
			Version = version;
		}

		public NewId BlobId { get; protected set; }
		public NewId AggregateId { get; private set; }
		public int Version { get; set; }
	}
}
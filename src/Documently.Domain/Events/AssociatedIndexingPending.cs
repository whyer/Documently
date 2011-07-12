using System;
using Documently.Domain.Domain;

namespace Documently.Domain.Events
{
	[Serializable]
	public class AssociatedIndexingPending : DomainEvent
	{
		public AssociatedIndexingPending()
		{
		}

		public AssociatedIndexingPending(DocumentState processingState, Guid blobId, Guid arId, uint version) : base(arId, version)
		{
			ProcessingState = processingState;
			BlobId = blobId;
		}

		public DocumentState ProcessingState { get; protected set; }
		public Guid BlobId { get; protected set; }
	}
}
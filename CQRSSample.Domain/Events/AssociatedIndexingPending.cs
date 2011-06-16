using System;
using Documently.Domain.Domain;

namespace Documently.Domain.Events
{
	public class AssociatedIndexingPending : DomainEvent
	{
		private readonly DocumentState _ProcessingState;
		private readonly Guid _BlobId;

		public AssociatedIndexingPending(DocumentState processingState, Guid blobId)
		{
			_ProcessingState = processingState;
			_BlobId = blobId;
		}

		public DocumentState ProcessingState
		{
			get {
				return _ProcessingState;
			}
		}

		public Guid BlobId
		{
			get { return _BlobId; }
		}
	}
}
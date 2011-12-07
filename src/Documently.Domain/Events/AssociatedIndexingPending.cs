using System;
using Documently.Domain.Domain;

namespace Documently.Domain.Events
{
	[Serializable]
	public class AssociatedIndexingPending : DomainEvent
	{
		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected AssociatedIndexingPending()
		{
		}

		public AssociatedIndexingPending(DocumentState processingState, Guid blobId, Guid arId, int version) : base(arId, version)
		{
			ProcessingState = processingState;
			BlobId = blobId;
		}

		public DocumentState ProcessingState { get; protected set; }
		public Guid BlobId { get; protected set; }
	}
}
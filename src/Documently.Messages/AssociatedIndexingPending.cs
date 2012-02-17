using System;

namespace Documently.Messages
{
	[Serializable]
	public class AssociatedIndexingPending : DomainEvent
	{
		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected AssociatedIndexingPending()
		{
		}

		public AssociatedIndexingPending(Guid blobId, Guid arId, int version) : base(arId, version)
		{
			BlobId = blobId;
		}

		public Guid BlobId { get; protected set; }
	}
}
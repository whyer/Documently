using System;

namespace Documently.Domain.Events
{
	[Serializable]
	public class AssociatedWithCollection : DomainEvent
	{
		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected AssociatedWithCollection()
		{
		}

		public AssociatedWithCollection(Guid documentId, Guid collectionId)
		{
			AggregateId = documentId;
			CollectionId = collectionId;
		}

		public Guid CollectionId { get; protected set; }
	}
}
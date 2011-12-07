using System;
using Documently.Domain.Domain;

namespace Documently.Domain.Events
{
	[Serializable]
	public class DocumentMetaDataCreated : DomainEvent
	{
		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected DocumentMetaDataCreated()
		{
		}

		public DocumentMetaDataCreated(Guid documentId, string title, DocumentState state, DateTime utcCreated)
		{
			Title = title;
			ProcessingState = state;
			UtcDate = utcCreated;
			AggregateId = documentId;
		}

		public string Title { get; protected set; }
		public DocumentState ProcessingState { get; protected set; }
		public DateTime UtcDate { get; protected set; }
	}
}
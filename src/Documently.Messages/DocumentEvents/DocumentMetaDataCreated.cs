using System;

namespace Documently.Messages
{
	[Serializable]
	public class DocumentMetaDataCreated : DomainEvent
	{
		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected DocumentMetaDataCreated()
		{
		}

		public DocumentMetaDataCreated(Guid documentId, string title, DateTime utcCreated)
		{
			Title = title;
			UtcDate = utcCreated;
			AggregateId = documentId;
		}

		public string Title { get; protected set; }
		public DateTime UtcDate { get; protected set; }
	}
}
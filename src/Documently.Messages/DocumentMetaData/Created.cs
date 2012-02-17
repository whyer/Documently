using System;

namespace Documently.Messages.DocumentMetaData
{
	[Serializable]
	public class Created : DomainEvent
	{
		public Created(Guid documentId, string title, DateTime utcCreated)
		{
			Title = title;
			UtcDate = utcCreated;
			AggregateId = documentId;
		}

		public Guid AggregateId { get; set; }
		public int Version { get; set; }
		public string Title { get; set; }
		public DateTime UtcDate { get; set; }
	}
}
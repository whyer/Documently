using System;

namespace Documently.Commands
{
	[Serializable]
	public class CreateDocumentMetaData : Command
	{
		public CreateDocumentMetaData(Guid arId, string title, DateTime utcTime)
		{
			Title = title;
			UtcTime = utcTime;
			AggregateId = arId;
		}

		public string Title { get; protected set; }
		public DateTime UtcTime { get; protected set; }
		
		public Guid AggregateId { get; set; }
		public int Version { get; set; }
	}
}
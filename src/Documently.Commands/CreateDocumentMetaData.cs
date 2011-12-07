using System;

namespace Documently.Commands
{
	[Serializable]
	public class CreateDocumentMetaData : Command
	{
		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected CreateDocumentMetaData()
		{
		}

		public CreateDocumentMetaData(Guid id, string title, DateTime utcTime) : base(id)
		{
			Title = title;
			UtcTime = utcTime;
		}

		public string Title { get; protected set; }
		public DateTime UtcTime { get; protected set; }
	}
}
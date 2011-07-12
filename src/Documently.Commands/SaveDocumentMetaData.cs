using System;

namespace Documently.Commands
{
	[Serializable]
	public class SaveDocumentMetaData : Command
	{
		private readonly string _Title;
		private readonly DateTime _UtcTime;

		public SaveDocumentMetaData()
		{
		}

		public SaveDocumentMetaData(Guid id, string title, DateTime utcTime) : base(id)
		{
			_Title = title;
			_UtcTime = utcTime;
		}

		public string Title
		{
			get { return _Title; }
		}

		public DateTime UtcTime
		{
			get { return _UtcTime; }
		}
	}
}
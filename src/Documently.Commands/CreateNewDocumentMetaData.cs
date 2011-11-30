using System;

namespace Documently.Commands
{
	[Serializable]
	public class CreateNewDocumentMetaData : Command
	{
		private string _Title;
		private DateTime _UtcTime;

        public CreateNewDocumentMetaData()
        {
        }

		public CreateNewDocumentMetaData(Guid id, string title, DateTime utcTime) : base(id)
		{
			_Title = title;
			_UtcTime = utcTime;
		}

		public string Title
		{
			get { return _Title; }
            protected set { _Title = value; }
		}

		public DateTime UtcTime
		{
			get { return _UtcTime; }
            protected set { _UtcTime = value; }
		}
	}
}
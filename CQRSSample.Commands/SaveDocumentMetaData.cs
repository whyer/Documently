using System;

namespace Documently.Commands
{
	[Serializable]
	public class SaveDocumentMetaData : Command
	{
		private readonly string _Name;
		private readonly DateTime _UtcTime;

		public SaveDocumentMetaData(Guid id, string name, DateTime utcTime) : base(id)
		{
			_Name = name;
			_UtcTime = utcTime;
		}

		public string Name
		{
			get { return _Name; }
		}

		public DateTime UtcTime
		{
			get { return _UtcTime; }
		}
	}
}
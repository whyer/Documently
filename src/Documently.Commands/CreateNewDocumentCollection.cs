using System;

namespace Documently.Commands
{
	[Serializable]
	public class CreateNewDocumentCollection : Command
	{
		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected CreateNewDocumentCollection()
		{
		}

		public CreateNewDocumentCollection(Guid id, string name)
		{
			AggregateId = id;
			Name = name;
		}

		public string Name { get; protected set; }
		public Guid AggregateId { get; set; }
		public int Version { get; set; }
	}
}
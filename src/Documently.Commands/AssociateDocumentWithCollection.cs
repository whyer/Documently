using System;

namespace Documently.Commands
{
	[Serializable]
	public class AssociateDocumentWithCollection : Command
	{
		public Guid CollectionId { get; protected set; }

		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected AssociateDocumentWithCollection()
		{
		}

		public AssociateDocumentWithCollection(Guid docId, Guid collectionId) : base(docId)
		{
			CollectionId = collectionId;
		}
	}
}
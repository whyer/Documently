using System;

namespace Documently.Domain.Events
{
	[Serializable]
	public class DocumentCollectionCreated : DomainEvent
	{
		public string Name { get; protected set; }

		/// <summary>
		/// 	for serialization
		/// </summary>
		[Obsolete("for serialization")]
		protected DocumentCollectionCreated()
		{
		}

		public DocumentCollectionCreated(Guid id, string name)
		{
			Name = name;
			AggregateId = id;
		}
	}
}
using Documently.Messages;
using Documently.Messages.DocumentCollection;
using Magnum;

namespace Documently.Domain
{
	public class DocumentCollection : AggregateRoot
	{
		public DocumentCollection()
		{
		}

		protected DocumentCollection(string collectionName)
		{
			var @event = new CollectionCreated(CombGuid.Generate(), collectionName);
			RaiseEvent(@event);
		}

		public void Apply(CollectionCreated evt)
		{
			Id = evt.AggregateId;
		}

		public static DocumentCollection CreateNew(string collectionName = "")
		{
			if (string.IsNullOrEmpty(collectionName))
			{
				collectionName = "Default name";
			}

			return new DocumentCollection(collectionName);
		}

		public void ApplyEvent<T>(T evt) where T : DomainEvent
		{
			throw new System.NotImplementedException();
		}
	}
}
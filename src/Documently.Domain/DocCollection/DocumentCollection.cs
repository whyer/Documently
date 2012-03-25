using Documently.Messages;
using Documently.Messages.DocCollectionEvents;
using Magnum;
using MassTransit;

namespace Documently.Domain
{
	public class DocumentCollection : AggregateRoot, EventAccessor
	{
		private EventRouter _eventRouter;

		private DocumentCollection()
		{
			_eventRouter = EventRouter.For(this);
		}

		protected DocumentCollection(string collectionName)
			: this()
		{
			this.Raise<DocumentCollection, Created>(new
				{
					Id = NewId.Next(),
					Name = collectionName
				});
		}

		public void Apply(Created evt)
		{
			Id = evt.AggregateId;
		}

		public static DocumentCollection CreateNew(string collectionName)
		{
			if (string.IsNullOrEmpty(collectionName))
				collectionName = "Default name";

			return new DocumentCollection(collectionName);
		}

		public void ApplyEvent<T>(T evt) where T : DomainEvent
		{
			throw new System.NotImplementedException();
		}

		public NewId Id { get; private set; }
		public uint Version { get; private set; }

		public EventRouter Events
		{
			get { return _eventRouter; }
		}
	}
}
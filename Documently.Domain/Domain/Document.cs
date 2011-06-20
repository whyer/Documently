using System;
using CommonDomain;
using CommonDomain.Core;
using Documently.Domain.Events;
using Magnum;

namespace Documently.Domain.Domain
{
	public class Document : AggregateBase<DomainEvent>
	{
		public Document()
		{
		}

		public Document(IRouteEvents<DomainEvent> handler) : base(handler)
		{
		}

		public Document(string title, DateTime utcCreated)
		{
			var @event = new DocumentMetaDataCreated(
				 CombGuid.Generate(), title, DocumentState.Created, utcCreated);

			Apply(@event);

			RaiseEvent(@event);
		}

		public void Apply(DocumentMetaDataCreated evt)
		{
			Id = evt.AggregateId;
		}

		public void AssociateWithDocumentBlob(Guid blobId)
		{
			var evt = new AssociatedIndexingPending(DocumentState.AssociatedIndexingPending, blobId, Id, (uint)Version + 1);

			RaiseEvent(evt);
		}
	}
}
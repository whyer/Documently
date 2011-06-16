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
			Id = CombGuid.Generate();

			RaiseEvent(new DocumentMetaDataCreated(
				Id, title, DocumentState.Created, utcCreated));
		}

		public void AssociateWithDocumentBlob(Guid blobId)
		{
			var evt = new AssociatedIndexingPending(DocumentState.AssociatedIndexingPending, blobId)
			{
				AggregateId = Id
			};

			RaiseEvent(evt);
		}

		public void Apply(DocumentMetaDataCreated evt)
		{
			Id = evt.AggregateId;
		}
	}
}
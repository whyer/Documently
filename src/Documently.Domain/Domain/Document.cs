using System;
using CommonDomain.Core;
using Documently.Domain.Events;
using Magnum;

namespace Documently.Domain.Domain
{
	public class Document : AggregateBase
	{
	    public Document()
		{
		}

		public Document(string title, DateTime utcCreated)
		{
			var @event = new DocumentMetaDataCreated(
				 CombGuid.Generate(), title, DocumentState.Created, utcCreated);

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

		public void Apply(AssociatedIndexingPending evt)
		{
			_documentBlobId = evt.BlobId;
		}

        public void AssociateWithCollection(Guid collectionId)
        {
            var @event = new AssociatedWithCollection(Id, collectionId, (uint) (Version + 1));
            RaiseEvent(@event);
        }

        public void Apply(AssociatedWithCollection @event)
        {}

        private Guid _documentBlobId;

	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using CommonDomain.Core;
using Documently.Messages;
using Magnum;

namespace Documently.Domain.Domain
{
	public class Document : AggregateBase
	{
		public Document()
		{
		}

		public Document(Guid documentId, string title, DateTime utcCreated)
		{
			var @event = new DocumentMetaDataCreated(
				documentId, title,  utcCreated);

			RaiseEvent(@event);
		}

		public void Apply(DocumentMetaDataCreated evt)
		{
			Id = evt.AggregateId;
		}

		public void AssociateWithDocumentBlob(Guid blobId)
		{
			var evt = new AssociatedIndexingPending( blobId, Id, Version + 1);
			RaiseEvent(evt);
		}

		public void Apply(AssociatedIndexingPending evt)
		{
			_documentBlobId = evt.BlobId;
		}

		public void AssociateWithCollection(Guid collectionId)
		{
			var @event = new AssociatedWithCollection(Id, collectionId);
			RaiseEvent(@event);
		}

        public void Apply(AssociatedWithCollection @event)
        { }

        public void Apply(DocumentSharedEvent @event)
        {
            Id = @event.AggregateId;
            Version = @event.Version;
        }

        private Guid _documentBlobId;

	    public void ShareWith(IEnumerable<int> userIDs)
	    {
            Contract.Requires(userIDs != null);
            Contract.Requires(userIDs.Count() > 0);
            Contract.Ensures(Contract.OldValue(userIDs) == userIDs);
            var @event = new DocumentSharedEvent(Id, Version, userIDs);
            RaiseEvent(@event);
	    }
	}
}
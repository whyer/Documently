using System;
using System.Collections.Generic;
using System.Linq;
using Documently.Commands;
using Documently.Domain.CommandHandlers;
using Documently.Domain.Domain;
using Documently.Domain.Events;
using Magnum;
using NUnit.Framework;
using SharpTestsEx;

namespace CQRSSample.Specs.Documents
{
    public class when_document_is_associated_with_a_collection
        : CommandTestFixture<AssociateDocumentWithCollection, DocumentAssociatedWithCollectionHandler, Document>
    {
        private Guid docId = CombGuid.Generate();
        private Guid collectionId = CombGuid.Generate();

        protected override IEnumerable<DomainEvent> Given()
        {
            return new List<DomainEvent>
                       {
                           new DocumentMetaDataCreated(docId, "title", DocumentState.Created, DateTime.UtcNow)
                       };
        }

        protected override AssociateDocumentWithCollection When()
        {
            return new AssociateDocumentWithCollection(docId, collectionId);
        }

        [Test]
        public void then_an_event_of_the_association_is_sent()
        {
            var evt = (AssociatedWithCollection)PublishedEventsT.First();
            evt.AggregateId.Should().Be(docId);
            evt.CollectionId.Should().Be(collectionId);
            evt.Version.Should().Be(1);
        }
    }

}
using System;
using System.Linq;
using Documently.Commands;
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

        protected override System.Collections.Generic.IEnumerable<DomainEvent> Given()
        {
            return new[] {new DocumentMetaDataCreated(docId, "title", DocumentState.Created, DateTime.UtcNow)};
        }

        protected override AssociateDocumentWithCollection When()
        {
            return new AssociateDocumentWithCollection(docId, collectionId);
        }

        [Test]
        public void then_the_document_records_the_collection_it_belongs_to()
        {
            var evt = (AssociatedWithCollection)PublishedEventsT.First();
            evt.AggregateId.Should().Be(docId);
            evt.CollectionId.Should().Be(collectionId);
        }
    }
}
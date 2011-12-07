using System;
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
	public class when_uploaded_document_itself
		: CommandTestFixture<InitializeDocumentIndexing, DocumentIndexingHandler, Document>
	{
		private Guid _DocId = CombGuid.Generate();

		protected override System.Collections.Generic.IEnumerable<DomainEvent> Given()
		{
			return new[] {new DocumentMetaDataCreated(_DocId, "My document", DocumentState.Created, DateTime.UtcNow)};
		}

		protected override InitializeDocumentIndexing When()
		{
			return new InitializeDocumentIndexing(_DocId, CombGuid.Generate());
		}

		[Test]
		public void then_the_document_should_take_note_of_the_associated_indexing_that_is_pending()
		{
			var evt = (AssociatedIndexingPending)PublishedEventsT.First();
			evt.AggregateId.Should().Be(_DocId);
			evt.ProcessingState.Should().Be(DocumentState.AssociatedIndexingPending);
		}
	}
}
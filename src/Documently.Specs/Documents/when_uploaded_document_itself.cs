using System;
using System.Linq;
using Documently.Commands;
using Documently.Domain.CommandHandlers;
using Documently.Domain.Domain;
using Documently.Messages;
using Documently.Messages.DocumentMetaData;
using Magnum;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_uploaded_document_itself
		: CommandTestFixture<InitializeDocumentIndexing, DocumentIndexingHandler, DocumentMetaData>
	{
		private Guid _DocId = CombGuid.Generate();

		protected override System.Collections.Generic.IEnumerable<DomainEvent> Given()
		{
			return new[] {new Created(_DocId, "My document",  DateTime.UtcNow)};
		}

		protected override InitializeDocumentIndexing When()
		{
			return new InitializeDocumentIndexing(_DocId, CombGuid.Generate());
		}

		[Test]
		public void then_the_document_should_take_note_of_the_associated_indexing_that_is_pending()
		{
			var evt = (DocumentUploaded)PublishedEventsT.First();
			evt.AggregateId.Should().Be(_DocId);
		}
	}
}
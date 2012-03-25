using System;
using System.Linq;
using Documently.Domain;
using Documently.Domain.CommandHandlers;
using Documently.Domain.CommandHandlers.ForDocMeta;
using Documently.Messages;
using Documently.Messages.DocMetaCommands;
using Documently.Messages.DocMetaEvents;
using Magnum;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_uploaded_document_itself
		: CommandTestFixture<AssociateWithDocument, DocumentIndexingHandler, DocMeta>
	{
		private NewId _DocId = CombNewId.Generate();

		protected override System.Collections.Generic.IEnumerable<DomainEvent> Given()
		{
			return new[] {new Created(_DocId, "My document",  DateTime.UtcNow)};
		}

		protected override AssociateWithDocument When()
		{
			return new AssociateWithDocument(_DocId, CombNewId.Generate());
		}

		[Test]
		public void then_the_document_should_take_note_of_the_associated_indexing_that_is_pending()
		{
			var evt = (DocumentUploaded)PublishedEventsT.First();
			evt.AggregateId.Should().Be(_DocId);
		}
	}
}
using System;
using System.Linq;
using Documently.Domain;
using Documently.Domain.CommandHandlers.ForDocMeta;
using Documently.Messages;
using Documently.Messages.DocMetaCommands;
using Documently.Messages.DocMetaEvents;
using MassTransit;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_uploaded_document_itself
		: CommandTestFixture<AssociateWithDocument, AssociateWithDocumentHandler, DocMeta>
	{
		private readonly NewId _docId = NewId.Next();

		protected override System.Collections.Generic.IEnumerable<DomainEvent> Given()
		{
			yield break;
			//return new[] {new Created(_docId, "My document",  DateTime.UtcNow)};
		}

		protected override AssociateWithDocument When()
		{
			return null;//new AssociateWithDocument(_docId, NewId.Next());
		}

		[Test]
		public void then_the_document_should_take_note_of_the_associated_indexing_that_is_pending()
		{
			var evt = (DocumentUploaded)PublishedEventsT.First();
			evt.AggregateId.Should().Be(_docId);
		}
	}
}
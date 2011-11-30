using System;
using Documently.Commands;
using Documently.Domain.CommandHandlers;
using Documently.Domain.Domain;
using Documently.Domain.Events;
using Magnum;
using NUnit.Framework;
using System.Linq;
using SharpTestsEx;

namespace CQRSSample.Specs.Documents
{
	public class when_creating_new_document_meta_data
		: CommandTestFixture<CreateNewDocumentMetaData, CreateDocumentMetaDataHandler, Document>
	{
		private readonly DateTime _Created = DateTime.UtcNow;

		protected override CreateNewDocumentMetaData When()
		{
			return new CreateNewDocumentMetaData(CombGuid.Generate(), "My document", _Created);
		}

		[Test]
		public void should_get_created_document_event()
		{
			var evt = (DocumentMetaDataCreated)PublishedEventsT.First();
			evt.Title.Should().Be("My document");
			evt.ProcessingState.Should().Be(DocumentState.Created);
			evt.UtcDate.Should().Be(_Created);
		}
	}
}
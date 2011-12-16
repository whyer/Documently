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
		: CommandTestFixture<CreateDocumentMetaData, DocumentMetaDataHandler, Document>
	{
		private readonly DateTime _created = DateTime.UtcNow;
	    private readonly Guid _documentId = CombGuid.Generate();

	    protected override CreateDocumentMetaData When()
		{
			return new CreateDocumentMetaData(_documentId, "My document", _created);
		}

		[Test]
		public void should_get_created_document_event()
		{
			var evt = (DocumentMetaDataCreated)PublishedEventsT.First();
			evt.Title.Should().Be("My document");
			evt.ProcessingState.Should().Be(DocumentState.Created);
			evt.UtcDate.Should().Be(_created);
		    evt.AggregateId.Should().Be(_documentId);
		}

	    
	}
}
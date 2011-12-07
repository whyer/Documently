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
	public class when_creating_new_documentCollection_no_name_given
		: CommandTestFixture<CreateNewDocumentCollection, CreateNewDocumentCollectionHandler, DocumentCollection>
	{
		protected override CreateNewDocumentCollection When()
		{
			return new CreateNewDocumentCollection(CombGuid.Generate(), string.Empty, 0);
		}

		[Test]
		public void should_use_default_name_if_none_given()
		{
			var @event = (DocumentCollectionCreated) PublishedEventsT.First();
			@event.Name.Should().Be("Default name");
		}
	}
}
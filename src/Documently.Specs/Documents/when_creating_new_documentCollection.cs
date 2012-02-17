using System.Linq;
using Documently.Commands;
using Documently.Domain.CommandHandlers;
using Documently.Domain.Domain;
using Documently.Messages.DocumentCollection;
using Magnum;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_creating_new_documentCollection
		: CommandTestFixture<CreateNewDocumentCollection, CreateNewDocumentCollectionHandler, DocumentCollection>
	{
		private string _collectionName = "Name";

		protected override CreateNewDocumentCollection When()
		{
			return new CreateNewDocumentCollection(CombGuid.Generate(), _collectionName);
		}

		[Test]
		public void should_recieve_new_collection_created_event()
		{
			var evt = (CollectionCreated) PublishedEventsT.First();
			evt.Name.Should().Be(_collectionName);
		}
	}
}
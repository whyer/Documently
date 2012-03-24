using System.Linq;
using Documently.Domain;
using Documently.Domain.CommandHandlers;
using Documently.Messages.DocCollectionCmds;
using Documently.Messages.DocCollectionEvents;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_creating_new_documentCollection
		: CommandTestFixture<Create, CreateNewDocumentCollectionHandler, DocumentCollection>
	{
		private string _collectionName = "Name";

		protected override Create When()
		{
			return new Create(NewId.Generate(), _collectionName);
		}

		[Test]
		public void should_recieve_new_collection_created_event()
		{
			var evt = (Created) PublishedEventsT.First();
			evt.Name.Should().Be(_collectionName);
		}
	}
}
using System.Linq;
using Documently.Commands;
using Documently.Domain;
using Documently.Domain.CommandHandlers;
using Documently.Messages.DocCollectionCmds;
using Documently.Messages.DocCollectionEvents;
using Magnum;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_creating_new_documentCollection_no_name_given
		: CommandTestFixture<Create, CreateNewDocumentCollectionHandler, DocumentCollection>
	{
		protected override Create When()
		{
			return new Create(NewId.Next(), string.Empty);
		}

		[Test]
		public void should_use_default_name_if_none_given()
		{
			var evt = (Created) PublishedEventsT.First();
			evt.Name.Should().Be("Default name");
		}
	}
}
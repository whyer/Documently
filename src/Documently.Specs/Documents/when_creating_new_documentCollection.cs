using System.Linq;
using Documently.Domain;
using Documently.Domain.CommandHandlers;
using Documently.Domain.CommandHandlers.ForDocCollection;
using Documently.Messages.DocCollectionCmds;
using Documently.Messages.DocCollectionEvents;
using MassTransit;
using MassTransit.Context;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_creating_new_documentCollection
		: CommandTestFixture<Create, CreateHandler, DocumentCollection>
	{
		private string _collectionName = "Name";

		protected override Create When()
		{
			return null;//new Create(NewId.Next(), _collectionName);
		}

		protected override void Consume(ConsumeContext<Create> cmd)
		{
			CommandHandler.Consume(cmd);
		}

		[Test]
		public void should_recieve_new_collection_created_event()
		{
			var evt = (Created) PublishedEventsT.First();
			evt.Name.Should().Be(_collectionName);
		}
	}
}
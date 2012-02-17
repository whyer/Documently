using System;
using System.Collections.Generic;
using System.Linq;
using Documently.Commands.DocumentMetaData;
using Documently.Domain.CommandHandlers;
using Documently.Domain.Domain;
using Documently.Messages;
using Documently.Messages.DocumentMetaData;
using Magnum;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_document_is_associated_with_a_collection
		: CommandTestFixture<AssociateDocumentWithCollection, DocumentAssociatedWithCollectionHandler, DocumentMetaData>
	{
		private readonly Guid docId = CombGuid.Generate();
		private readonly Guid collectionId = CombGuid.Generate();

		protected override IEnumerable<DomainEvent> Given()
		{
			return new List<DomainEvent>
				{
					new Created(docId, "title",  DateTime.UtcNow)
				};
		}

		protected override AssociateDocumentWithCollection When()
		{
			return new AssociateDocumentWithCollection(docId, collectionId);
		}

		[Test]
		public void then_an_event_of_the_association_is_sent()
		{
			var evt = (AssociatedWithCollection) PublishedEventsT.First();
			evt.AggregateId.Should().Be(docId);
			evt.CollectionId.Should().Be(collectionId);
		}
	}
}
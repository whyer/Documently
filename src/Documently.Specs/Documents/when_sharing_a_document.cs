using System.Collections.Generic;
using System.Linq;
using Documently.Domain;
using Documently.Domain.CommandHandlers.ForDocMeta;
using Documently.Messages;
using Documently.Messages.DocCollectionCmds;
using Documently.Messages.DocMetaEvents;
using MassTransit;
using MassTransit.Context;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_sharing_a_document : CommandTestFixture<ShareDocument, ShareDocumentHandler, DocMeta>
	{
		readonly List<int> _userIDs = new List<int> {1, 2, 3};
		readonly NewId _documentId = NewId.Next();

		protected override IEnumerable<DomainEvent> Given()
		{
			return new List<DomainEvent>
				{
					//new Created(_documentId, "",  DateTime.UtcNow)
				};
		}

		protected override ShareDocument When()
		{
			return null; //new ShareDocument(NewId.Next(), 1, _userIDs);
		}

		protected override void Consume(ConsumeContext<ShareDocument> cmd)
		{
			CommandHandler.Consume(cmd);
		}

		[Test]
		public void Then_a_document_shared_event_will_be_pulished()
		{
			Assert.AreEqual(typeof (WasShared), PublishedEvents.Last().GetType());
		}

		[Test]
		public void Then_user_ids_is_in_events()
		{
			Event().UserIds.SequenceEqual(_userIDs).Should().Be(true);
		}

		public WasShared Event()
		{
			return (WasShared) PublishedEvents.Last();
		}
	}
}

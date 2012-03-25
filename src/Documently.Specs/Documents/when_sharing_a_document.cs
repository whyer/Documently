using System;
using System.Collections.Generic;
using Documently.Domain;
using Documently.Domain.CommandHandlers;
using Documently.Messages;
using Documently.Messages.DocCollectionCmds;
using Documently.Messages.DocMetaEvents;
using Magnum;
using NUnit.Framework;
using System.Linq;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
    public class when_sharing_a_document : CommandTestFixture<ShareDocument, ShareDocumentCommandHandler, DocMeta>
    {
    	private readonly List<int> _userIDs = new List<int> {1, 2, 3};
    	private readonly NewId _documentId = CombNewId.Generate();

    	protected override IEnumerable<DomainEvent> Given()
    	{
    		return new List<DomainEvent>
    		       	{
						new Created(_documentId, "",  DateTime.UtcNow)
					};
    	}

    	protected override ShareDocument When()
        {
            return new ShareDocument(CombNewId.Generate(), 1, _userIDs);
        }

    	[Test]
        public void Then_a_document_shared_event_will_be_pulished()
        {
            Assert.AreEqual(typeof(WasShared), PublishedEvents.Last().GetType());
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

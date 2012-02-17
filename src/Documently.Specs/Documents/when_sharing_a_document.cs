using System;
using System.Collections.Generic;
using Documently.Commands;
using Documently.Domain.CommandHandlers;
using Documently.Domain.Domain;
using Documently.Messages;
using Documently.Messages.DocumentMetaData;
using Magnum;
using NUnit.Framework;
using System.Linq;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
    public class when_sharing_a_document : CommandTestFixture<ShareDocument, ShareDocumentCommandHandler, DocumentMetaData>
    {
    	private readonly List<int> _userIDs = new List<int> {1, 2, 3};
    	private readonly Guid _documentId = CombGuid.Generate();

    	protected override IEnumerable<DomainEvent> Given()
    	{
    		return new List<DomainEvent>
    		       	{
						new Created(_documentId, "",  DateTime.UtcNow)
					};
    	}

    	protected override ShareDocument When()
        {
            return new ShareDocument(CombGuid.Generate(), 1, _userIDs);
        }

    	[Test]
        public void Then_a_document_shared_event_will_be_pulished()
        {
            Assert.AreEqual(typeof(Shared), PublishedEvents.Last().GetType());
        }

    	[Test]
    	public void Then_user_ids_is_in_events()
    	{
    		Event().UserIds.SequenceEqual(_userIDs).Should().Be(true);
    	}

		public Shared Event()
		{
			return (Shared) PublishedEvents.Last();
		}
    }
}

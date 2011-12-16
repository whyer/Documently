
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Documently.Commands;
using Documently.Domain.CommandHandlers;
using Documently.Domain.Domain;
using Documently.Domain.Events;
using Magnum;
using Magnum.Collections;
using NUnit.Framework;

namespace CQRSSample.Specs.Documents
{

    public class when_sharing_a_document : CommandTestFixture<ShareDocument, ShareDocumentCommandHandler, Document>
    {
        protected override ShareDocument When()
        {
            return new ShareDocument();
        }

        protected override IEnumerable<DomainEvent> Given()
        {
            IEnumerable<int> userIDs = new List<int>(){1, 2, 3};
            return new List<DomainEvent>(){new DocumentSharedEvent(CombGuid.Generate(), 1, userIDs)};
        }

        [Test]
        public void Then_a_document_shared_event_will_be_pulished()
        {
            Assert.AreEqual(typeof(DocumentSharedEvent), PublishedEvents.Last().GetType());

        }
    }
}

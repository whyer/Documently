using Documently.Domain.Events;
using Raven.Client;

namespace Documently.ReadModel
{
        public class DocumentListView : HandlesEvent<DocumentMetaDataCreated>, HandlesEvent<DocumentCollectionCreated>
        {
            private readonly IDocumentStore _DocumentStore;

            public DocumentListView(IDocumentStore documentStore)
            {
                _DocumentStore = documentStore;
            }

            public void Consume(DocumentMetaDataCreated @event)
            {
                using (var session = _DocumentStore.OpenSession())
                {
                    var dto = new DocumentDto
                                  {
                                      AggregateRootId = @event.AggregateId,
                                      Title = @event.Title,
                                      CreatedUtc = @event.UtcDate
                                  };

                    session.Store(dto);
                    session.SaveChanges();
                }
            }


            public void Consume(DocumentCollectionCreated message)
            {
                using(var session = _DocumentStore.OpenSession())
                {
                    var dto = new DocumentCollectionDto
                                  {
                                      AggregateRootId = message.AggregateId,
                                      Name = message.Name
                                  };

                    session.Store(dto);
                    session.SaveChanges();
                }
            }
        }
}
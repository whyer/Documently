using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
    public class CreateNewDocumentCollectionHandler : Consumes<CreateNewDocumentCollection>.All
    {
        private readonly Func<IRepository> _reposistory;

        public CreateNewDocumentCollectionHandler(Func<IRepository> reposistory)
        {
            if(reposistory == null)
                throw new ArgumentNullException("reposistory");
            _reposistory = reposistory;
        }

        public void Consume(CreateNewDocumentCollection message)
        {
            var collection = DocumentCollection.CreateNew(message.Name);
            var repository = _reposistory();
            repository.Save(collection, Guid.NewGuid());
        }
    }
}
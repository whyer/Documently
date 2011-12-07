using System;
using System.Management.Instrumentation;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
    public class DocumentAssociatedWithCollectionHandler : Consumes<AssociateDocumentWithCollection>.All
    {
        private readonly Func<IRepository> _repository;
    	private readonly IServiceBus _bus;

    	public DocumentAssociatedWithCollectionHandler(Func<IRepository> repository, IServiceBus bus)
        {
            if(repository == null) throw new ArgumentNullException("repository");
            _repository = repository;
        	_bus = bus;
        }

        public void Consume(AssociateDocumentWithCollection message)
        {
            var repository = _repository();
            var document = repository.GetById<Document>(message.Id, message.Version);
        	_bus.Context(ctx =>
        		{
        			document.AssociateWithCollection(message.CollectionId);
        			repository.Save(document, CombGuid.Generate(), null);
        		});
        }
    }
}
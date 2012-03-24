using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.CommandHandlers.Infrastructure;
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
    public class ShareDocumentCommandHandler : Consumes<ShareDocument>.All
    {
        private Func<DomainRepository> _repository;

        public ShareDocumentCommandHandler(Func<DomainRepository> repository)
        {
            _repository = repository;
            
        }
        public void Consume(ShareDocument command)
        {
            var repo = _repository();
            const int version = 0;
            var document = repo.GetById<DocMeta>(command.AggregateId, version);
            document.ShareWith(command.UserIds);
            repo.Save(document, CombNewId.Generate(), null);
            
        }
    }
}
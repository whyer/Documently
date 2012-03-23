using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
    public class ShareDocumentCommandHandler : Consumes<ShareDocument>.All
    {
        private Func<IRepository> _repository;

        public ShareDocumentCommandHandler(Func<IRepository> repository)
        {
            _repository = repository;
            
        }
        public void Consume(ShareDocument command)
        {
            var repo = _repository();
            const int version = 0;
            var document = repo.GetById<DocumentMetaData>(command.AggregateId, version);
            document.ShareWith(command.UserIds);
            repo.Save(document, CombGuid.Generate(), null);
            
        }
    }
}
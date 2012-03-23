using System;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Messages.DocCollectionCmds;
using Magnum;
using MassTransit;
using System.Linq;

namespace Documently.Domain.CommandHandlers
{
	public class CreateNewDocumentCollectionHandler : Consumes<IConsumeContext<Create>>.All
    {
        private readonly Func<DomainRepository> _reposistory;

		public CreateNewDocumentCollectionHandler(Func<DomainRepository> reposistory)
        {
            if(reposistory == null)
                throw new ArgumentNullException("reposistory");
            _reposistory = reposistory;
        }

        public void Consume(IConsumeContext<Create> context)
        {
			var collection = DocumentCollection.CreateNew(context.Message.Name);
            var repository = _reposistory();
            repository.Save(collection, context.MessageId, 
				context.Headers.ToDictionary(x => x.Key, x => x.Value));
        }
    }
}
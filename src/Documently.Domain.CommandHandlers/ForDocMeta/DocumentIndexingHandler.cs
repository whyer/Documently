using System;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Messages.DocMetaCommands;
using MassTransit;

namespace Documently.Domain.CommandHandlers.ForDocMeta
{
	public class DocumentIndexingHandler : Consumes<IConsumeContext<InitializeDocumentIndexing>>.All
	{
		private readonly Func<DomainRepository> _repo;

		public DocumentIndexingHandler(Func<DomainRepository> repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_repo = repo;
		}

		public void Consume(IConsumeContext<InitializeDocumentIndexing> context)
		{
			var repo = _repo();
			var command = context.Message;
			var doc = repo.GetById<DocMeta>(command.AggregateId, command.Version);
		
			doc.AssociateWithDocumentBlob(command.BlobId);

			repo.Save(doc, context.GetMessageId(), context.GetHeaders());
		}
	}
}
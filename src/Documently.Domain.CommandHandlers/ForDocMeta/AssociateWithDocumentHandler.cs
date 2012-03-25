using System;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Messages.DocMetaCommands;
using MassTransit;

namespace Documently.Domain.CommandHandlers.ForDocMeta
{
	public class AssociateWithDocumentHandler : Consumes<IConsumeContext<AssociateWithDocument>>.All
	{
		private readonly Func<DomainRepository> _repo;

		public AssociateWithDocumentHandler(Func<DomainRepository> repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_repo = repo;
		}

		public void Consume(IConsumeContext<AssociateWithDocument> context)
		{
			var repo = _repo();
			var command = context.Message;
			var doc = repo.GetById<DocMeta>(command.AggregateId, command.Version);
		
			doc.AssociateWithData(command.Data);

			repo.Save(doc, context.GetMessageId(), context.GetHeaders());
		}
	}
}
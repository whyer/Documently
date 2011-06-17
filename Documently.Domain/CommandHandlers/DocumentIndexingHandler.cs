using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public class DocumentIndexingHandler : Handles<InitializeDocumentIndexing>
	{
		private readonly IRepository _Repo;
		//private readonly IServiceBus _Bus;

		public DocumentIndexingHandler(IRepository repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_Repo = repo;
			//_Bus = bus;
		}

		public void Handle(InitializeDocumentIndexing command)
		{
			var doc = _Repo.GetById<Document>(command.Id, command.Version);
			doc.AssociateWithDocumentBlob(command.BlobId);
			//_Bus.Context().Respond();
			_Repo.Save(doc, CombGuid.Generate(), null);
		}
	}
}
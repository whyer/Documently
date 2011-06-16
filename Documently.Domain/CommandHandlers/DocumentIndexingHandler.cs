using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using Magnum;

namespace Documently.Domain.CommandHandlers
{
	public class DocumentIndexingHandler : Handles<InitializeDocumentIndexing>
	{
		private readonly IRepository _Repo;

		public DocumentIndexingHandler(IRepository repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_Repo = repo;
		}

		public void Handle(InitializeDocumentIndexing command)
		{
			var doc = _Repo.GetById<Document>(command.Id, command.Version);
			doc.AssociateWithDocumentBlob(command.BlobId);
			_Repo.Save(doc, CombGuid.Generate(), null);
		}
	}
}
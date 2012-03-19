using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public class DocumentIndexingHandler : Consumes<InitializeDocumentIndexing>.All
	{
		private readonly Func<IRepository> _Repo;
		//private readonly IServiceBus _Bus;

		public DocumentIndexingHandler(Func<IRepository> repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_Repo = repo;
			//_Bus = bus;
		}

		public void Consume(InitializeDocumentIndexing command)
		{
			var repo = _Repo();
			var doc = repo.GetById<DocumentMetaData>(command.AggregateId, command.Version);
			doc.AssociateWithDocumentBlob(command.BlobId);
			//_Bus.Context().Respond();
			repo.Save(doc, CombGuid.Generate(), null);
		}
	}
}
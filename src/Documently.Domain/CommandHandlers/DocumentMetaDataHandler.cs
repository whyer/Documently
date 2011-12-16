using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public class DocumentMetaDataHandler : Consumes<CreateDocumentMetaData>.All
	{
		private readonly Func<IRepository> _repo;

		public DocumentMetaDataHandler(Func<IRepository> repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_repo = repo;
		}

		public void Consume(CreateDocumentMetaData command)
		{
		    var document = new Document(command.ArId, command.Title, command.UtcTime);
		    _repo().Save(document, CombGuid.Generate(), null);
		}
	}
}
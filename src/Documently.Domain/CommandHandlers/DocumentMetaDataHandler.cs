using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public class DocumentMetaDataHandler : Consumes<CreateDocumentMetaData>.All
	{
		private readonly Func<IRepository> _Repo;

		public DocumentMetaDataHandler(Func<IRepository> repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_Repo = repo;
		}

		public void Consume(CreateDocumentMetaData command)
		{
			_Repo().Save(new Document(command.Title, command.UtcTime), command.Id, null);
		}
	}
}
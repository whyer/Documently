using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public class DocumentMetaDataHandler : Handles<SaveDocumentMetaData>
	{
		private readonly IRepository _Repo;

		public DocumentMetaDataHandler(IRepository repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_Repo = repo;
		}

		public void Consume(SaveDocumentMetaData command)
		{
			_Repo.Save(new Document(command.Title, command.UtcTime), command.Id, null);
		}
	}
}
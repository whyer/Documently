using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public class DocumentMetaDataHandler : Consumes<SaveDocumentMetaData>.All, Handles<SaveDocumentMetaData>
	{
		private readonly IRepository _Repo;

		public DocumentMetaDataHandler(IRepository repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_Repo = repo;
		}

		public void Handle(SaveDocumentMetaData command)
		{
			_Repo.Save(new Document(command.Title, command.UtcTime), CombGuid.Generate(), null);
		}

		public void Consume(SaveDocumentMetaData message)
		{
			Handle(message);
		}
	}
}
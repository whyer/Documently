using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Messages.DocMetaCommands;
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public class DocumentMetaDataHandler : Consumes<Create>.All
	{
		private readonly Func<IRepository> _repo;

		public DocumentMetaDataHandler(Func<IRepository> repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_repo = repo;
		}

		public void Consume(Create command)
		{
		    var document = new DocumentMetaData(command.AggregateId, command.Title, command.UtcTime);
		    _repo().Save(document, CombGuid.Generate(), null);
		}
	}
}
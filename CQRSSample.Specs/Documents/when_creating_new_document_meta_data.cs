using System;
using CQRSSample.Domain.CommandHandlers;
using CQRSSample.Domain.Events;
using CommonDomain.Core;
using CommonDomain.Persistence;
using Documently.Commands;
using Magnum;
using MassTransit;

namespace CQRSSample.Specs.Documents
{
	public class when_creating_new_document_meta_data
		: CommandTestFixture<SaveDocumentMetaData, DocumentMetaDataHandler, Document>
	{
		protected override SaveDocumentMetaData When()
		{
			return new SaveDocumentMetaData(CombGuid.Generate(), "My document", DateTime.UtcNow);
		}
	}

	public class DocumentMetaDataHandler : Consumes<SaveDocumentMetaData>, Handles<SaveDocumentMetaData>
	{
		private readonly IRepository _Repo;

		public DocumentMetaDataHandler(IRepository repo)
		{
			if (repo == null) throw new ArgumentNullException("repo");
			_Repo = repo;
		}

		public void Handle(SaveDocumentMetaData command)
		{
			_Repo.Save(new Document(
				
				), CombGuid.Generate(), null);
		}
	}

	public class Document : AggregateBase<DomainEvent>
	{
	}
}
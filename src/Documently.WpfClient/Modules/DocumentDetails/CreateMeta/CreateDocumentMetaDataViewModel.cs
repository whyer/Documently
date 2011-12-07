using System;
using Caliburn.Micro;
using Documently.Commands;
using Documently.Infrastructure;

namespace Documently.WpfClient.Modules.DocumentDetails.CreateMeta
{
	public class CreateDocumentMetaDataViewModel : Screen
	{
		private readonly IBus _Bus;
		private readonly IEventAggregator _EventAggregator;

		public CreateDocumentMetaDataViewModel(IBus bus, IEventAggregator eventAggregator)
		{
			_Bus = bus;
			_EventAggregator = eventAggregator;
			Command = new SaveDocumentMetaDataModel();
		}

		protected SaveDocumentMetaDataModel Command { get; private set; }

		public void Save()
		{
			_Bus.Send(new CreateDocumentMetaData(Guid.NewGuid(), Command.Title, DateTime.UtcNow));
			_EventAggregator.Publish(new DocumentMetaDataSaved());
			//_Bus.RegisterHandler((DocumentMetaDataCreated evt) => _EventAggregator.Publish(evt)); // TODO: set this up better!
		}
	}

	public class SaveDocumentMetaDataModel
	{
		public string Title { get; set; }
	}

	public class DocumentMetaDataSaved
	{}
}
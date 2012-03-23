using System;
using Caliburn.Micro;
using Documently.Infrastructure;
using Documently.Messages.DocMetaCommands;
using Magnum;

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
			_Bus.Send(new CreateImpl(CombGuid.Generate(), Command.Title, DateTime.UtcNow));
			_EventAggregator.Publish(new DocumentMetaDataSaved());
		}
	}

	class CreateImpl : Create
	{
		public CreateImpl(Guid aggregateId, uint version, string title, DateTime utcTime)
		{
			AggregateId = aggregateId;
			Version = version;
			Title = title;
			UtcTime = utcTime;
		}

		public Guid AggregateId { get; set; }
		public uint Version { get; set; }
		public string Title { get; set; }
		public DateTime UtcTime { get; set; }
	}


	public class SaveDocumentMetaDataModel
	{
		public string Title { get; set; }
	}

	public class DocumentMetaDataSaved
	{}
}
using System;
using Caliburn.Micro;
using MassTransit;

namespace Documently.WpfClient.Modules.DocumentSearch
{
	public class DocumentSearchViewModel : Screen
	{
		private readonly IServiceBus _Bus;
		private readonly IEventAggregator _EventAggregator;

		public DocumentSearchViewModel(IServiceBus bus, IEventAggregator eventAggregator)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
			_Bus = bus;
			_EventAggregator = eventAggregator;
		}


	}
}
using System;
using Caliburn.Micro;
using Documently.Infrastructure;
using MassTransit;

namespace Documently.WpfClient.Modules.DocumentSearch
{
	public class DocumentSearchViewModel : Screen
	{
		private readonly IBus _Bus;
		private readonly IEventAggregator _EventAggregator;

		public DocumentSearchViewModel(IBus bus, IEventAggregator eventAggregator)
		{
			if (bus == null) throw new ArgumentNullException("bus");
			if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
			_Bus = bus;
			_EventAggregator = eventAggregator;
		}


	}
}
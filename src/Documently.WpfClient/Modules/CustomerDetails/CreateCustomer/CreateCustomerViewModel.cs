using System;
using Caliburn.Micro;
using Documently.Commands;
using Documently.Infrastructure;
using MassTransit;

namespace Documently.WpfClient.Modules.CustomerDetails.CreateCustomer
{
	public class CreateCustomerViewModel : Screen
	{
		private readonly IServiceBus _Bus;
		private readonly IEventAggregator _EventAggregator;

		public CreateCustomerViewModel(IServiceBus bus, IEventAggregator eventAggregator)
		{
			_Bus = bus;
			_EventAggregator = eventAggregator;
			Command = new CreateNewCustomer(Guid.NewGuid());
		}

		public CreateNewCustomer Command { get; private set; }

		public void Save()
		{
			//important: send command over bus
			_Bus.Publish(Command);

			//signal for UI - change view
			_EventAggregator.Publish(new CreateCustomerSavedEvent());
		}
	}

	public class CreateCustomerSavedEvent
	{
	}
}
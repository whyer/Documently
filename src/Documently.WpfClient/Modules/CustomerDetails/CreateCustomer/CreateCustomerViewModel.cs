using System;
using Caliburn.Micro;
using Documently.Infrastructure;
using Documently.Messages.CustCommands;
using Documently.WpfClient.Commands;
using Documently.WpfClient.Modules.CustomerDetails.CustomerRelocating;
using Magnum;
using MassTransit;

namespace Documently.WpfClient.Modules.CustomerDetails.CreateCustomer
{
	public class CreateCustomerViewModel : Screen
	{
		private readonly IBus _bus;
		private readonly IEventAggregator _eventAggregator;

		public CreateCustomerViewModel(IBus bus, IEventAggregator eventAggregator)
		{
			_bus = bus;
			_eventAggregator = eventAggregator;
			Command = new RegisterNewImpl
				{
					Version = 0,
					AggregateId = CombGuid.Generate(),
					Address = new AddressImpl()
				};
		}

		public RegisterNew Command { get; private set; }

		public void Save()
		{
			//important: send command over bus
			_bus.Send(Command);

			//signal for UI - change view
			_eventAggregator.Publish(new CreateCustomerSavedEvent());
		}
	}

	public class CreateCustomerSavedEvent
	{
	}
}
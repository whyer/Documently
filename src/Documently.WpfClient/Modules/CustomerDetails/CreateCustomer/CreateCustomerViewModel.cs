using System;
using Caliburn.Micro;
using Documently.Infrastructure;
using Documently.Messages.CustCommands;
using Magnum;

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
			Command = new RegisterNewImpl(CombGuid.Generate());
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

	public class RegisterNewImpl : RegisterNew
	{
		public RegisterNewImpl(Guid arId)
		{
			AggregateId = arId;
			Version = 0;
		}

		public Guid AggregateId { get; set; }
		public uint Version { get; set; }
		public string CustomerName { get; set; }
		public string Street { get; set; }
		public string StreetNumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
		public string PhoneNumber { get; set; }
	}

	public class CreateCustomerSavedEvent
	{
	}
}
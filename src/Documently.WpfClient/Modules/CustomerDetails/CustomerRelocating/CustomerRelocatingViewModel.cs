using System;
using Caliburn.Micro;
using Documently.Commands;
using Documently.Infrastructure;
using Documently.ReadModel;
using Documently.WpfClient.ApplicationFramework;
using Documently.WpfClient.CommandValidation;

namespace Documently.WpfClient.Modules.CustomerDetails.CustomerRelocating
{
	public class CustomerRelocatingViewModel : ScreenWithValidatingCommand<RelocateTheCustomer>
	{
		private readonly IBus _bus;
		private readonly IEventAggregator _eventAggregator;
		private readonly IReadRepository _readRepository;

		public CustomerRelocatingViewModel(IBus bus, IEventAggregator eventAggregator, IReadRepository readRepository)
		{
			_bus = bus;
			_eventAggregator = eventAggregator;
			_readRepository = readRepository;

			Validator = new RelocateTheCustomerValidator();
		}

		public void WithCustomer(Guid customerId)
		{
			ViewModel = _readRepository.GetById<CustomerAddressDto>(Dto.GetDtoIdOf<CustomerAddressDto>(customerId));

			Command = new ValidatingCommand<RelocateTheCustomer>(
				new RelocateImpl(ViewModel.AggregateRootId,
					ViewModel.LatestVersionSeen, 
					ViewModel.Street, 
					ViewModel.StreetNumber, ViewModel.PostalCode, ViewModel.City),
				Validator);
		}

		public CustomerAddressDto ViewModel { get; private set; }

		public string Address
		{
			get
			{
				return string.Format("{0} {1}, {2} {3}", ViewModel.Street, ViewModel.StreetNumber, ViewModel.PostalCode,
				                     ViewModel.City);
			}
		}

		public void Save()
		{
			if (!Validate().IsValid)
				return;

			//important: send command over bus
			_bus.Send(Command.InnerCommand);

			//signal for UI - change view
			_eventAggregator.Publish(new CustomerRelocatingSavedEvent());
		}
	}

	class RelocateImpl : RelocateTheCustomer
	{
		public RelocateImpl(Guid aggregateId, uint version, string street, string streetnumber, string postalCode, string city)
		{
			AggregateId = aggregateId;
			Version = version;
			Street = street;
			Streetnumber = streetnumber;
			PostalCode = postalCode;
			City = city;
		}

		public Guid AggregateId { get; set; }
		public uint Version { get; set; }
		public string Street { get; set; }
		public string Streetnumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
	}

	public class CustomerRelocatingSavedEvent
	{
	}
}
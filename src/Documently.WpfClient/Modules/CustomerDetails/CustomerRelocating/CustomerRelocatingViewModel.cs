// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using Caliburn.Micro;
using Documently.Infrastructure;
using Documently.Messages.CustCommands;
using Documently.ReadModel;
using Documently.WpfClient.ApplicationFramework;
using Documently.WpfClient.CommandValidation;
using MassTransit;

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

		public void WithCustomer(NewId customerId)
		{
			ViewModel = _readRepository.GetById<CustomerAddressDto>(Dto.GetDtoIdOf<CustomerAddressDto>(customerId));

			Command = new ValidatingCommand<RelocateTheCustomer>(
				new RelocateImpl
					{
						AggregateId = ViewModel.AggregateId,
						Version = ViewModel.LatestVersionSeen + 1,
						NewAddress = new AddressImpl
							{
								Street = ViewModel.Street,
								StreetNumber = ViewModel.StreetNumber,
								PostalCode = ViewModel.PostalCode,
								City = ViewModel.City
							}
					},
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

	public class AddressImpl : Address
	{
		public string Street { get; set; }
		public uint StreetNumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
	}

	internal class RelocateImpl : RelocateTheCustomer
	{
		public NewId AggregateId { get; set; }
		public uint Version { get; set; }
		public Address NewAddress { get; set; }
	}

	public class CustomerRelocatingSavedEvent
	{
	}
}
using System;
using CommonDomain.Core;
using CQRSSample.Domain.Events;

namespace CQRSSample.Domain.Domain
{
	public class Customer : AggregateBase<DomainEvent>
	{
		private bool _deactivated;
		private CustomerName _customerName;
		private Address _address;
		private PhoneNumber _phoneNumber;

		private Customer(Guid id, CustomerName customerName, Address address, PhoneNumber phoneNumber)
		{
			RaiseEvent(new CustomerCreatedEvent(id, customerName.Name, address.Street, address.StreetNumber, address.PostalCode,
			                                    address.City, phoneNumber.Number));
		}

		public Customer()
		{
		}

		public void RelocateCustomer(string street, string streetNumber, string postalCode, string city)
		{
			if (Id == Guid.Empty)
				throw new NonExistingCustomerException("The customer is not created and no opperations can be executed on it");

			RaiseEvent(new CustomerRelocatedEvent(Id, street, streetNumber, postalCode, city));
		}

		public static Customer CreateNew(Guid id, CustomerName customerName, Address address, PhoneNumber phoneNumber)
		{
			return new Customer(id, customerName, address, phoneNumber);
		}


		//Domain-Eventhandlers
		private void Apply(CustomerCreatedEvent @event)
		{
			Id = @event.AggregateId;
			_customerName = new CustomerName(@event.CustomerName);
			_address = new Address(@event.Street, @event.StreetNumber, @event.PostalCode, @event.City);
			_phoneNumber = new PhoneNumber(@event.PhoneNumber);
		}

		private void Apply(CustomerRelocatedEvent @event)
		{
			_address = new Address(@event.Street, @event.StreetNumber, @event.PostalCode, @event.City);
		}
	}
}
using System;
using CommonDomain.Core;
using Documently.Messages;
using Documently.Messages.CustomerEvents;

namespace Documently.Domain.Domain
{
	public class Customer : AggregateBase
    {
		private Customer(Guid id, CustomerName customerName, Address address, PhoneNumber phoneNumber)
		{
			RaiseEvent(new Created(id, customerName.Name, address.Street, address.StreetNumber, address.PostalCode,
			                                    address.City, phoneNumber.Number));
		}

		public Customer()
		{
		}

		public void RelocateCustomer(string street, string streetNumber, string postalCode, string city)
		{
			if (Id == Guid.Empty)
				throw new NonExistingCustomerException("The customer is not created and no opperations can be executed on it");

			RaiseEvent(new Relocated(Id, street, streetNumber, postalCode, city));
		}

		public static Customer CreateNew(Guid id, CustomerName customerName, Address address, PhoneNumber phoneNumber)
		{
			return new Customer(id, customerName, address, phoneNumber);
		}

		//Domain-Eventhandlers
		private void Apply(Created @event)
		{
			Id = @event.AggregateId;
			// we don't need to keep any other state here.
		}

		private void Apply(Relocated @event)
		{
			// neither do we here, at this point in time since we've already sent the event.
			//new Address(@event.Street, @event.StreetNumber, @event.PostalCode, @event.City);
		}
	}
}
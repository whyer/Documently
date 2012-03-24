using Documently.Messages.CustEvents;
using MassTransit;
using MassTransit.Util;

namespace Documently.Domain
{
	public class Customer : AggregateRoot, EventAccessor
	{
		private readonly EventRouter _eventRouter;

		private Customer()
		{
			_eventRouter = EventRouter.For(this);
		}

		private Customer(NewId id, CustomerName customerName, Address address, PhoneNumber phoneNumber)
			: this()
		{
			this.Raise<Customer, Created>(new
				{
					Id = id,
					customerName.Name,
					address.Street,
					address.StreetNumber,
					address.PostalCode,
					address.City,
					phoneNumber.Number
				});
		}

		public void RelocateCustomer(string street, string streetNumber, string postalCode, string city)
		{
			if (Id == NewId.Empty)
				throw new NonExistingCustomerException(
					"The customer is not created and no opperations can be executed on it");

			this.Raise<Customer, Relocated>(new
				{
					Id,
					Street = street,
					StreetNumber = streetNumber,
					PostalCode = postalCode,
					City = city
				});
		}

		public NewId Id { get; private set; }
		public uint Version { get; private set; }

		[UsedImplicitly]
		private void Apply(Created evt)
		{
			Id = evt.AggregateId;
		}

		[UsedImplicitly]
		private void Apply(Relocated evt)
		{
			// neither do we here, at this point in time since we've already sent the event.
			//new Address(evt.Street, evt.StreetNumber, evt.PostalCode, evt.City);
		}

		EventRouter EventAccessor.Events
		{
			get { return _eventRouter; }
		}

		public static Customer CreateNew(NewId id, CustomerName customerName, Address address, PhoneNumber phoneNumber)
		{
			return new Customer(id, customerName, address, phoneNumber);
		}
	}
}
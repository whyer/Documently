using Documently.Messages.CustEvents;
using MassTransit;
using MassTransit.Util;

namespace Documently.Domain
{
	/// <summary>
	/// internal small classes that are simple event interface implementations
	/// </summary>
	class RegisteredImpl
		: Registered
	{
		public NewId AggregateId { get; set; }
		public uint Version { get;  set; }
		public string CustomerName { get; set; }
		public Messages.CustDtos.Address Address { get; set; }
		public string PhoneNumber { get; set; }
	}

    /// <summary>
    /// internal small classes that are simple event interface implementations
    /// </summary>
    class RelocatedImpl : Relocated
    {
        public NewId AggregateId { get; set; }
        public uint Version { get; set; }
        public string Street { get; set; }
        public uint StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }

	public class Customer : AggregateRoot, EventAccessor
	{
		private readonly EventRouter _eventRouter;

		// for construction in tests
		internal Customer()
		{
			_eventRouter = EventRouter.For(this);
		}

		private Customer(NewId id, CustomerName customerName, Address address, PhoneNumber phoneNumber)
			: this()
		{
			this.Raise<Customer, Registered>(new RegisteredImpl
				{
					AggregateId = id,
					Version = Version + 1,
					CustomerName = customerName.Name,
					Address = new Address(address.Street, address.StreetNumber, address.PostalCode, address.City),
					PhoneNumber = phoneNumber.Number
				});
		}

		public void RelocateCustomer(string street, uint streetNumber, string postalCode, string city)
		{
			if (Id == NewId.Empty)
				throw new NonExistingCustomerException(
					"The customer is not created and no opperations can be executed on it");

			this.Raise<Customer, Relocated>(new RelocatedImpl()
				{
					AggregateId = Id,
					Street = street,
					StreetNumber = streetNumber,
					PostalCode = postalCode,
					City = city,
					Version = Version + 1
				});
		}

		public NewId Id { get; private set; }
		public uint Version { get; private set; }

		[UsedImplicitly]
		internal void Apply(Registered evt)
		{
			Id = evt.AggregateId;
		}

		[UsedImplicitly]
		internal void Apply(Relocated evt)
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
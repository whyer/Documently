using System;

namespace Documently.Domain.Events
{
	[Serializable]
	public class CustomerCreatedEvent : DomainEvent
	{
		public string CustomerName { get; protected set; }
		public string Street { get; protected set; }
		public string StreetNumber { get; protected set; }
		public string PostalCode { get; protected set; }
		public string City { get; protected set; }
		public string PhoneNumber { get; protected set; }

		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected CustomerCreatedEvent()
		{
		}

		public CustomerCreatedEvent(Guid id, string customerName, string street, string streetNumber, string postalCode,
		                            string city, string phoneNumber)
		{
			AggregateId = id;
			CustomerName = customerName;
			Street = street;
			StreetNumber = streetNumber;
			PostalCode = postalCode;
			City = city;
			PhoneNumber = phoneNumber;
		}
	}
}
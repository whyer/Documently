using System;

namespace Documently.Messages.CustomerEvents
{
	[Serializable]
	public class Created : DomainEvent
	{
		public Created(Guid id, string customerName, string street, string streetNumber, string postalCode,
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

		public string CustomerName { get; protected set; }
		public string Street { get; protected set; }
		public string StreetNumber { get; protected set; }
		public string PostalCode { get; protected set; }
		public string City { get; protected set; }
		public string PhoneNumber { get; protected set; }


		public Guid AggregateId { get; private set; }
		public int Version { get; set; }
	}
}
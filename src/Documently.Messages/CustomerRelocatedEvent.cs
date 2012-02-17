using System;

namespace Documently.Messages
{
	[Serializable]
	public class CustomerRelocatedEvent : DomainEvent
	{
		public string Street { get; protected set; }
		public string StreetNumber { get; protected set; }
		public string PostalCode { get; protected set; }
		public string City { get; protected set; }

		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected CustomerRelocatedEvent()
		{
		}

		public CustomerRelocatedEvent(Guid id, string street, string streetNumber, string postalCode, string city)
		{
			AggregateId = id;
			Street = street;
			StreetNumber = streetNumber;
			PostalCode = postalCode;
			City = city;
		}
	}
}
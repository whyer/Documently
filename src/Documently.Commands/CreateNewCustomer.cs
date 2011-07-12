using System;

namespace Documently.Commands
{
	[Serializable]
	public class CreateNewCustomer : Command
	{
		public string CustomerName { get; set; }
		public string Street { get; set; }
		public string StreetNumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
		public string PhoneNumber { get; set; }

		public CreateNewCustomer()
		{
		}

		public CreateNewCustomer(Guid id) : base(id)
		{
		}

		public CreateNewCustomer(Guid id, string customerName, string street, string streetNumber, string postalCode,
		                             string city, string phoneNumber)
			: base(id)
		{
			CustomerName = customerName;
			Street = street;
			StreetNumber = streetNumber;
			PostalCode = postalCode;
			City = city;
			PhoneNumber = phoneNumber;
		}
	}
}
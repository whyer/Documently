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

		/// <summary>
		/// 	for serialization
		/// </summary>
		[Obsolete("for serialization")]
		protected CreateNewCustomer()
		{
		}

		public CreateNewCustomer(Guid arId, string customerName, string street, string streetNumber, string postalCode,
		                         string city, string phoneNumber)
			: base(arId)
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
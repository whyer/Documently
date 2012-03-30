namespace Documently.Domain
{
	public class Address
		: Messages.CustDtos.Address
	{
		public Address(string street, uint number, string code, string city)
		{
			Street = street;
			StreetNumber = number;
			PostalCode = code;
			City = city;
		}

		public string Street { get; private set; }
		public uint StreetNumber { get; private set; }
		public string PostalCode { get; private set; }
		public string City { get; private set; }
	}
}
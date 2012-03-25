namespace Documently.Domain
{
	public class Address
	{
		public readonly string Street;
		public readonly uint StreetNumber;
		public readonly string PostalCode;
		public readonly string City;

		public Address(string street, uint number, string code, string city)
		{
			Street = street;
			StreetNumber = number;
			PostalCode = code;
			City = city;
		}
	}
}
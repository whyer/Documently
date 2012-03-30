namespace Documently.Messages.CustDtos
{
	public interface Address
	{
		string Street { get; }
		uint StreetNumber { get; }
		string PostalCode { get; }
		string City { get; }
	}
}
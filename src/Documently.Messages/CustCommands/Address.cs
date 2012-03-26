namespace Documently.Messages.CustCommands
{
	public interface Address
	{
		string Street { get; }
		uint StreetNumber { get; }
		string PostalCode { get; }
		string City { get; }
	}
}
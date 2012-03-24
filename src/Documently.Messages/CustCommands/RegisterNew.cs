using Documently.Commands;

namespace Documently.Messages.CustCommands
{
	public interface RegisterNew : Command
	{
		string CustomerName { get; }
		string PhoneNumber { get; }
		Address Address { get; }
	}

	public interface Address
	{
		string Street { get; set; }
		string StreetNumber { get; set; }
		string PostalCode { get; set; }
		string City { get; set; }
	}
}
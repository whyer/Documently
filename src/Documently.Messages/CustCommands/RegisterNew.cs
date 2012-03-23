using Documently.Commands;

namespace Documently.Messages.CustCommands
{
	public interface RegisterNew : Command
	{
		string CustomerName { get; set; }
		string Street { get; set; }
		string StreetNumber { get; set; }
		string PostalCode { get; set; }
		string City { get; set; }
		string PhoneNumber { get; set; }
	}
}
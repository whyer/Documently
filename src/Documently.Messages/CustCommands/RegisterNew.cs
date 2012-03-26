namespace Documently.Messages.CustCommands
{
	public interface RegisterNew : Command
	{
		string CustomerName { get; }
		string PhoneNumber { get; }
		Address Address { get; }
	}
}
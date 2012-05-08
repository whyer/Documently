using Documently.Messages.CustDtos;

namespace Documently.Messages.CustCommands
{
	public interface RelocateTheCustomer : Command
	{
		Address NewAddress { get; }
	}
}
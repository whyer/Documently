using Documently.Messages.CustDtos;

namespace Documently.Messages.CustEvents
{
	public interface Registered : DomainEvent
	{
		string CustomerName { get; }
		Address Address { get; }
		string PhoneNumber { get; }
	}
}
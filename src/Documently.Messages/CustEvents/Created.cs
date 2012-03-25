namespace Documently.Messages.CustEvents
{
	public interface Created : DomainEvent
	{
		string CustomerName { get; }
		string Street { get; }
		uint StreetNumber { get; }
		string PostalCode { get; }
		string City { get; }
		string PhoneNumber { get; }
	}
}
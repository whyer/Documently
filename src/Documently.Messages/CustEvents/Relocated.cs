namespace Documently.Messages.CustEvents
{
	public interface Relocated : DomainEvent
	{
		string Street { get; }
		string StreetNumber { get; }
		string PostalCode { get; }
		string City { get; }
	}
}
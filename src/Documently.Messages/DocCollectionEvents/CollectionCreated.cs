
namespace Documently.Messages.DocCollectionEvents
{
	public interface Created : DomainEvent
	{
		string Name { get; }
	}
}
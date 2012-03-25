using MassTransit;

namespace Documently.Messages
{
	public interface Command
	{
		NewId AggregateId { get; set; }
		uint Version { get; set; }
	}
}
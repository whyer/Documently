using System;

namespace Documently.Messages
{
	public interface Command
	{
		Guid AggregateId { get; set; }
		uint Version { get; set; }
	}
}
using System;

namespace Documently.Commands
{
	public interface Command
	{
		Guid AggregateId { get; set; }
		uint Version { get; set; }
	}
}
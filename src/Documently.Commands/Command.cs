using System;

namespace Documently.Commands
{
	public interface Command
	{
		Guid AggregateId { get; set; }
		int Version { get; set; }
	}
}
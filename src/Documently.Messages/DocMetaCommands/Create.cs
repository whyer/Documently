using System;

namespace Documently.Messages.DocMetaCommands
{
	public interface Create : Command
	{
		string Title { get; set; }
		DateTime UtcTime { get; set; }
	}
}
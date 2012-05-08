using System;

namespace Documently.Messages.DocMetaCommands
{
	public interface AssociateWithDocument : Command
	{
		Uri Data { get; }
	}
}
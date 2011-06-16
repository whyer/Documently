// ReSharper disable InconsistentNaming

using Documently.Commands;

namespace Documently.Domain.CommandHandlers
{
	public interface Handles<in T> : Handles where T : Command
	{
		void Handle(T command);
	}

	public interface Handles
	{
	}
}
// ReSharper restore InconsistentNaming
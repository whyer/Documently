using Documently.Messages;

namespace Documently.Infrastructure
{
	/// <summary>
	/// Simple wrapping interface. An abstraction of the service bus that may or may not be needed. In this case
	/// we provide it because it can be beneficial to have this abstraction in a fat client that may need to replace
	/// the actual service bus.
	/// </summary>
	public interface IBus
	{
		void Send<T>(T command)
			where T : class, Command;
	}
}
// ReSharper disable InconsistentNaming

using Documently.Commands;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public interface Handles<T> : Consumes<T>.All, Handles where T : Command
	{
	}

	public interface Handles
	{
	}
}
// ReSharper restore InconsistentNaming
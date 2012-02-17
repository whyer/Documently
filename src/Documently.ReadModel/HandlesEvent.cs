using Documently.Messages;
using MassTransit;

namespace Documently.ReadModel
{
	public interface HandlesEvent<T> : Consumes<T>.All where T : class, DomainEvent
	{
	}
}
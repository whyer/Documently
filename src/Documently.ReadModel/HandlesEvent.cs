using Documently.Domain.Events;
using MassTransit;

namespace Documently.ReadModel
{
	public interface HandlesEvent<T> : Consumes<T>.All where T : DomainEvent
	{
	}
}
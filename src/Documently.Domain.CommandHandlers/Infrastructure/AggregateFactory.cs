using System;
using MassTransit;

namespace Documently.Domain.CommandHandlers.Infrastructure
{
	public class AggregateFactory : AggregateRootFactory
	{
		public AggregateRoot Build(Type type, NewId id, Memento snapshot)
		{
			return Activator.CreateInstance(type) as AggregateRoot;
		}
	}

	public interface AggregateRootFactory
	{
		AggregateRoot Build(Type type, NewId id, Memento snapshot);
	}

	public interface Memento
	{
	}
}
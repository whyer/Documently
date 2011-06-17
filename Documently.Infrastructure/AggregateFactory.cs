using System;
using CommonDomain;
using CommonDomain.Persistence;

namespace Documently.Infrastructure
{
	public class AggregateFactory : IConstructAggregates
	{
		public IAggregate Build(Type type, Guid id, IMemento snapshot)
		{
			//return Activator.CreateInstance(type, id) as IAggregate; 
			return Activator.CreateInstance(type) as IAggregate; // todo
		}
	}
}
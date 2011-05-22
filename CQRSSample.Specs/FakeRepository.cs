using System;
using System.Collections.Generic;
using CommonDomain;
using CommonDomain.Persistence;
using System.Linq;

namespace CQRSSample.Specs
{
	public class FakeRepository : IRepository
	{
		public IAggregate SavedAggregate { get; set; }

		public TAggregate GetById<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate
		{
			var aggregate = Activator.CreateInstance(typeof(TAggregate)) as TAggregate;
			return aggregate;
		}

		public void Save(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders)
		{
			SavedAggregate = aggregate;
		}
	}
}
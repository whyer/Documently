using System;
using System.Collections.Generic;
using CommonDomain;
using CommonDomain.Persistence;
using System.Linq;

namespace CQRSSample.Specs
{
	public class FakeRepository : IRepository
	{
		private readonly object _Instance;
		public IAggregate SavedAggregate { get; set; }

		public FakeRepository()
		{
		}

		public FakeRepository(object instance)
		{
			_Instance = instance;
		}

		public TAggregate GetById<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate
		{
			var aggregate = (TAggregate)_Instance ?? Activator.CreateInstance(typeof(TAggregate)) as TAggregate;
			return aggregate;
		}

		public void Save(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders)
		{
			SavedAggregate = aggregate;
		}
	}
}
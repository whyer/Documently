using System;
using System.Collections.Generic;
using CommonDomain;
using CommonDomain.Persistence;

namespace Documently.Specs
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


        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IAggregate
        {
            return GetById<TAggregate>(id, 0);
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
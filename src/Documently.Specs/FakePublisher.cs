using System;
using System.Collections.Generic;
using System.Linq;
using Documently.Commands;
using Documently.Infrastructure;
using Documently.Messages;
using EventStore;
using EventStore.Dispatcher;

namespace CQRSSample.Specs
{
	public class FakePublisher : IDispatchCommits
	{
		private readonly ICollection<DomainEvent> _domainEvents;
		private List<Command> _sent = new List<Command>();

		public FakePublisher(ICollection<DomainEvent> domainEvents)
		{
			_domainEvents = domainEvents;
		}

		public void Dispose()
		{
		}

		public void Dispatch(Commit commit)
		{
			commit.Events.ToList().ForEach(e => _domainEvents.Add(e.Body as DomainEvent));
		}
	}
}
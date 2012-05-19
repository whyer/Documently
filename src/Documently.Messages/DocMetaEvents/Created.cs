using System;
using MassTransit;

namespace Documently.Messages.DocMetaEvents
{
	public interface Created 
		: DomainEvent, CorrelatedBy<Guid>
	{
		string Title { get;  }
		DateTime UtcDate { get; }
	}
}
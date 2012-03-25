using System;

namespace Documently.Messages.DocMetaEvents
{
	public interface Created : DomainEvent
	{
		string Title { get;  }
		DateTime UtcDate { get; }
	}
}
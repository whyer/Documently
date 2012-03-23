using System;
using MassTransit;

namespace Documently.Messages.Indexer
{
	public interface IndexingCompleted
		: CorrelatedBy<Guid>
	{
		Guid DocumentId { get; }
	}
}
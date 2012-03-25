using System;
using MassTransit;

namespace Documently.Messages.Indexer
{
	public interface IndexingCompleted
		: CorrelatedBy<NewId>
	{
		NewId DocumentId { get; }
	}
}
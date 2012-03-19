using System;

namespace Documently.Messages.Indexer
{
	public interface IndexingCompleted
	{
		Guid DocumentId { get; }
	}
}
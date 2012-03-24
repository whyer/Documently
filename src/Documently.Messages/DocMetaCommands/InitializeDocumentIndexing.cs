using System;

namespace Documently.Messages.DocMetaCommands
{
	public interface InitializeDocumentIndexing : Command
	{
		NewId BlobId { get; }
	}
}
using System;
using Documently.Messages.DocMetaCommands;

namespace Documently.Domain.CommandHandlers.Tests.MsgImpl
{
	class CreateDocumentMeta : Create
	{
		public Guid AggregateId { get; set; }
		public uint Version { get; set; }
		public string Title { get; set; }
		public DateTime UtcTime { get; set; }
	}
}
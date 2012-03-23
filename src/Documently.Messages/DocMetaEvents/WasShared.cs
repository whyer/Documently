using System.Collections.Generic;

namespace Documently.Messages.DocMetaEvents
{
	/// <summary>
	/// The document(meta data) was shared with a range of users, specified by <see cref="SharedWithUserIds"/>.
	/// </summary>
	public interface WasShared : DomainEvent
	{
		IEnumerable<int> SharedWithUserIds { get; }
	}
}
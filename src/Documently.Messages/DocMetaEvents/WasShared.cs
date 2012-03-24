using System.Collections.Generic;

namespace Documently.Messages.DocMetaEvents
{
	/// <summary>
	/// The document(meta data) was shared with a range of users, specified by <see cref="UserIds"/>.
	/// </summary>
	public interface WasShared : DomainEvent
	{
		/// <summary>
		/// Gets the user ids that the document was shared with.
		/// </summary>
		IEnumerable<int> UserIds { get; }
	}
}
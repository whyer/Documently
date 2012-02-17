using System;
using System.Collections.Generic;

namespace Documently.Messages.DocumentMetaData
{
	/// <summary>
	/// The document(meta data) was shared with a range of users, specified by <see cref="UserIds"/>.
	/// </summary>
	[Serializable]
	public class Shared : DomainEvent
	{
		public Shared(Guid aggregateId, int version, IEnumerable<int> userIds)
		{
			AggregateId = aggregateId;
			Version = version;
			UserIds = userIds;
		}

		public IEnumerable<int> UserIds { get; protected set; }
		public Guid AggregateId { get; private set; }
		public int Version { get; set; }
	}
}
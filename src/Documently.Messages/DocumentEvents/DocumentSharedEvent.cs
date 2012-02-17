using System;
using System.Collections.Generic;

namespace Documently.Messages
{
    [Serializable]
    public class DocumentSharedEvent : DomainEvent
    {
    	[Obsolete("for serialization")]
        protected DocumentSharedEvent()
        {
        }

		public DocumentSharedEvent(Guid arId, int arVersion, IEnumerable<int> userIds) 
            : base(arId, arVersion)
		{
			UserIds = userIds;
		}

    	public IEnumerable<int> UserIds { get; protected set; }
    }
}
using System;
using System.Collections.Generic;
using Documently.Domain.Events;

namespace Documently.Domain.Events
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
using System;
using System.Collections.Generic;
using Documently.Domain.Events;

namespace Documently.Domain.Events
{
    [Serializable]
    public class DocumentSharedEvent : DomainEvent
    {
        public DocumentSharedEvent()
        {
        
        }
		public DocumentSharedEvent(Guid arId, int arVersion, IEnumerable<int> userIDs) 
            : base(arId, arVersion)
		{

        }
    }
}
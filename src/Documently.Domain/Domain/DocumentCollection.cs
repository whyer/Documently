using System;
using CommonDomain.Core;
using Documently.Domain.Events;

namespace Documently.Domain.Domain
{
    public class DocumentCollection : AggregateBase
    {
        public DocumentCollection()
        {
            var @event = new DocumentCollectionCreated(Id, "Default name");
            RaiseEvent(@event);
        }

        public void Apply(DocumentCollectionCreated evt)
        {
            this.Id = evt.AggregateId;
        }

        
    }

    public class DocumentCollectionCreated : DomainEvent
    {
        public string Name { get; protected set; }

        public DocumentCollectionCreated(Guid id, string name)
        {
            Name = name;
            AggregateId = id;
        }
    }
}
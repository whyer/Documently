using System;

namespace Documently.Domain.Events
{
    [Serializable]
    public class DocumentCollectionCreated : DomainEvent
    {
        public string Name { get; protected set; }

        public DocumentCollectionCreated()
        {}

        public DocumentCollectionCreated(Guid documentCollectionId, string name)
        {
            Name = name;
            AggregateId = documentCollectionId;
        }
    }
}
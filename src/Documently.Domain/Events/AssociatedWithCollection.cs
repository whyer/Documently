using System;

namespace Documently.Domain.Events
{
    [Serializable]
    public class AssociatedWithCollection : DomainEvent
    {
        public AssociatedWithCollection()
        {}

        public AssociatedWithCollection(Guid documentId, Guid collectionId)
        {
            AggregateId = documentId;
            CollectionId = collectionId;
        }

        public Guid CollectionId { get; private set; }
    }
}
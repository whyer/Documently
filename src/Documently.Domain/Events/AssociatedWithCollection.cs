using System;

namespace Documently.Domain.Events
{
    [Serializable]
    public class AssociatedWithCollection : DomainEvent
    {
        public AssociatedWithCollection()
        {}

        public AssociatedWithCollection(Guid documentId, Guid collectionId, uint version) : base(documentId, version)
        {
            CollectionId = collectionId;
        }

        public Guid CollectionId { get; protected set; }
    }
}
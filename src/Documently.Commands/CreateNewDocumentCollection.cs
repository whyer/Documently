using System;

namespace Documently.Commands
{
    [Serializable]
    public class CreateNewDocumentCollection : Command
    {
        public CreateNewDocumentCollection()
        {}

        public CreateNewDocumentCollection(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Name { get; protected set; }
    }
}
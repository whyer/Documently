using System;
using System.Collections.Generic;

namespace Documently.ReadModel
{
    public class DocumentCollectionDto : Dto
    {
        public string Name { get; set; }
        public IList<DocumentDto> DocumentList;

        public DocumentCollectionDto()
        {
            DocumentList = new List<DocumentDto>();
        }
    }

    public class DocumentDto : Dto
    {
        public string Title { get; set; }
        public DateTime CreatedUtc { get; set; }
    }
}
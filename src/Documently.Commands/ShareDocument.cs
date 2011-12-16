using System;
using System.Collections.Generic;

namespace Documently.Commands
{
    [Serializable]
    public class ShareDocument : Command
    {
        public IEnumerable<int> UserIDs { get; set; }
    }
}
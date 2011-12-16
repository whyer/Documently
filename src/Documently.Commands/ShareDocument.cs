using System;
using System.Collections.Generic;

namespace Documently.Commands
{
    [Serializable]
    public class ShareDocument : Command
    {
		[Obsolete("for serialization")]
    	protected ShareDocument()
    	{
    	}

    	public ShareDocument(Guid arId, int version, IEnumerable<int> userIds) : base(arId, version)
    	{
    		UserIds = userIds;
    	}

    	public IEnumerable<int> UserIds { get; protected set; }
    }
}
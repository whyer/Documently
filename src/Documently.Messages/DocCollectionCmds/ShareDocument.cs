using System;
using System.Collections.Generic;
using Documently.Messages;

namespace Documently.Commands
{
    [Serializable]
    public class ShareDocument : Command
    {
    	public ShareDocument(NewId arId, uint version, IEnumerable<int> userIds)
    	{
    		UserIds = userIds;
    		AggregateId = arId;
    		Version = version;
    	}

    	public IEnumerable<int> UserIds { get; protected set; }
    	public NewId AggregateId { get; set; }
    	public uint Version { get; set; }
    }
}
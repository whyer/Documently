
using System;
using Documently.Messages.DocMetaCommands;
using Magnum;
using MassTransit;
using MassTransit.Util;

namespace Documently.WebApp.Handlers.DocumentMetaData
{
    public class PostHandler
    {
    	readonly IServiceBus _bus;

    	public PostHandler([NotNull] IServiceBus bus)
    	{
    		_bus = bus;
    	}

    	public String Execute(DocumentMetaDataModel metaData)
        {
			_bus.Publish<Create>(new CreateImpl
			{
				AggregateId = CombGuid.Generate(),
				Version = 0,
				Title = metaData.Name,
				UtcTime = DateTime.UtcNow
			});

            return "Creating " + metaData.Name;
        }
    }
	internal class CreateImpl : Create
	{
		public Guid AggregateId { get; set; }
		public uint Version { get; set; }
		public string Title { get; set; }
		public DateTime UtcTime { get; set; }
	}

}
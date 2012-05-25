using System;
using System.Web;
using Documently.ReadModel;
using Documently.WebApp.Handlers.DocumentMetaData;
using MassTransit;
using MassTransit.NLogIntegration;
using MassTransit.Pipeline.Inspectors;
using Raven.Client;
using Raven.Client.Document;
using StructureMap;

namespace Documently.WebApp
{
	public class Bootstrapper
	{
		public static IContainer CreateContainer()
		{
			var container = new Container(
				x =>
				{
					x.For<IDocumentStore>().Use(new DocumentStore { ConnectionStringName = "RavenDB" }.Initialize());
					x.For(typeof (Consumes<>.All));
					x.Scan(a =>
					{
						a.AssemblyContainingType<IReadRepository>();
						a.AddAllTypesOf<IReadRepository>();
						//a.AddAllTypesOf(typeof(Consumes<>.All));
					});
					x.For<IServiceBus>().Singleton();
					x.ForConcreteType<PostHandler>().Configure.Ctor<IServiceBus>();
			//        x.ForConcreteType<PostHandler>().Configure.Ctor<IEndpoint>()
			//.Is(ctx => ctx.GetInstance<IServiceBus>().GetEndpoint(new Uri(endpointUri)));
				});

			var serviceBus = ServiceBusFactory.New(sbc =>
			{
				sbc.ReceiveFrom("rabbitmq://localhost/Documently.WebApp");
				sbc.UseJsonSerializer();
				sbc.UseRabbitMqRouting();
				sbc.UseNLog();
				sbc.Subscribe(x => x.LoadFrom(container));
			});

			PipelineViewer.Trace(serviceBus.InboundPipeline);

			//PipelineViewer.Trace(serviceBus.OutboundPipeline);

			container.Inject(serviceBus);
			return container;
		}
	}
}
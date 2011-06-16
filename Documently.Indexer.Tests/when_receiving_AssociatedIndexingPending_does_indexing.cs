using System;
using System.IO;
using System.Text;
using Documently.Domain.Domain;
using Documently.Domain.Events;
using Magnum;
using MassTransit;
using MassTransit.Pipeline;
using NUnit.Framework;
using SharpTestsEx;
using Directory = System.IO.Directory;

namespace Documently.Indexer.Tests
{
	public class when_receiving_AssociatedIndexingPending_does_indexing
	{
		private string _Tmp = "IndexerService";
		private Guid _BlobId;

		private string _Document =
			@"As you can see, we used the underscore symbol when creating an instance of the generic type, because the 
type inference algorithm in F# can deduce the type argument from the code (in this example it infers that the type 
argument is string, because the Add method is called with a string as an argument). After creating an instance we used 
Add method to modify the list and add two new items. Finally, we used a Seq.to_list function to convert the collection 
to the F# list.

As a fully compatible .NET language, F# also provides a way for declaring its own classes (called object types in F#), 
which are compiled into CLR classes or interfaces and therefore the types can be accessed from any other .NET language 
as well as used to extend classes written in other .NET languages. This is an important feature that allows accessing 
complex .NET libraries like Windows Forms or ASP.NET from F#.";

		[SetUp]
		public void given()
		{
			if (!Directory.Exists(_Tmp))
				Directory.CreateDirectory(_Tmp);

			_BlobId = CombGuid.Generate();

			File.WriteAllText(Path.Combine(_Tmp, _BlobId.ToString()), _Document, Encoding.UTF8);
		}

		[TearDown]
		public void then_finally()
		{
			if (Directory.Exists(_Tmp))
				Directory.Delete(_Tmp, true);
		}

		[Test]
		public void and_returns_correct_events()
		{
			var busIn = new Bus();
			Consumes<AssociatedIndexingPending>.All s = new IndexerService(busIn);
			s.Consume(new AssociatedIndexingPending(DocumentState.AssociatedIndexingPending, _BlobId));
			busIn.Published.Should();
		}
	}

	// Note: this will be replaced by the testing framework at "MassTransit.Test" in version 2.0 of the bus.
	class Bus : IServiceBus
	{
		private object _Published;

		public void Dispose()
		{
		}

		public object Published
		{
			get { return _Published; }
		}

		public void Publish<T>(T message, Action<IPublishContext<T>> contextCallback) where T : class
		{
			_Published = message;
		}

		public TService GetService<TService>() where TService : IBusService
		{
			throw new NotImplementedException();
		}

		public IEndpoint GetEndpoint(Uri address)
		{
			throw new NotImplementedException();
		}

		public UnsubscribeAction Configure(Func<IInboundPipelineConfigurator, UnsubscribeAction> configure)
		{
			throw new NotImplementedException();
		}

		public IEndpoint Endpoint
		{
			get { throw new NotImplementedException(); }
		}

		public IInboundMessagePipeline InboundPipeline
		{
			get { throw new NotImplementedException(); }
		}

		public IOutboundMessagePipeline OutboundPipeline
		{
			get { throw new NotImplementedException(); }
		}

		public IServiceBus ControlBus
		{
			get { throw new NotImplementedException(); }
		}

		public IEndpointCache EndpointCache
		{
			get { throw new NotImplementedException(); }
		}
	}
}

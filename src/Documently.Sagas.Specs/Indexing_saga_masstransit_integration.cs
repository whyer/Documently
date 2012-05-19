using System;
using System.Linq;
using System.Threading;
using Documently.Messages.DocMetaCommands;
using Documently.Messages.DocMetaEvents;
using Documently.Messages.Indexer;
using Magnum.Extensions;
using MassTransit;
using MassTransit.AutomatonymousTests;
using MassTransit.Saga;
using MassTransit.Services.Timeout.Messages;
using MassTransit.SubscriptionConfigurators;
using NUnit.Framework;
using Automatonymous;

namespace Documently.Sagas.Specs
{
	[TestFixture]
	public class Indexing_saga_masstransit_integration
		:MassTransitTestFixture
	{
		[Test]
		public void Should_handle_created_event()
		{
			Guid sagaId = Guid.NewGuid();

			Bus.Publish(new TestCreatedEvent
			{
			    CorrelationId = sagaId
			});

			Instance instance = _repository.ShouldContainSagaInState(sagaId, _machine.IndexingPending, 8.Seconds());
			Assert.IsNotNull(instance);
		}

		[Test]
		public void Should_handle_indexingStarted_event()
		{
			Guid sagaId = Guid.NewGuid();

			Bus.Publish(new TestCreatedEvent
			{
				CorrelationId = sagaId,
			});

			Bus.Publish(new TestIndexerStarted
			{
				CorrelationId = sagaId
			});

			Instance instance = _repository.ShouldContainSagaInState(sagaId, _machine.Indexing, 8.Seconds());
			Assert.IsNotNull(instance);
		}

		[Test]
		public void Should_publish_TimeoutMessage_when_indexingStarted()
		{
			Guid sagaId = Guid.NewGuid();

			var messageReceived = new FutureMessage<ScheduleTimeout>();
			Bus.SubscribeHandler<ScheduleTimeout>(messageReceived.Set);

			Bus.Publish(new TestCreatedEvent
			{
			    CorrelationId = sagaId,
			});

			var indexerStarted = new TestIndexerStarted
			{
			    CorrelationId = sagaId
			};
			Bus.Publish(indexerStarted);

			Assert.IsTrue(messageReceived.IsAvailable(8.Seconds()));
			Assert.AreEqual(indexerStarted.CorrelationId, messageReceived.Message.CorrelationId);
		}

		class TestCreatedEvent : Created
		{
			public Guid AggregateId { get; set; }
			public uint Version { get; private set; }
			public string Title { get; private set; }
			public DateTime UtcDate { get; private set; }

			public Guid CorrelationId { get; set; }
		}

		class TestIndexerStarted : Documently.Messages.Indexer.Started
		{
			public Guid CorrelationId { get; set; }
		}

		InMemorySagaRepository<Instance> _repository;
		IndexerOrchestrationSaga _machine;

		protected override void ConfigureSubscriptions(SubscriptionBusServiceConfigurator configurator)
		{
			_repository = new InMemorySagaRepository<Instance>();
			 _machine = new IndexerOrchestrationSaga();

			configurator.StateMachineSaga(_machine, _repository);
		}
		 
	}
}
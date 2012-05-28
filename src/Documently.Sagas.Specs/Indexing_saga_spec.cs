using Automatonymous;
using Automatonymous.Impl;
using Documently.Messages.DocMetaEvents;
using Documently.Messages.Indexer;
using Machine.Fakes;
using Magnum;
using MassTransit;
using MassTransit.Services.Timeout;
using MassTransit.Testing;

#pragma warning disable 169
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace Documently.Sagas.Specs
{
	using System;
	using Machine.Specifications;
	using It = Machine.Specifications.It;

	// machine specifications howto:
	// https://github.com/machine/machine.specifications
	// sample: https://github.com/joliver/EventStore/blob/master/src/tests/EventStore.Core.UnitTests/OptimisticEventStreamTests.cs

	[Subject(typeof (IndexerOrchestrationSaga))]
	public class Indexing_saga_MetaDataCreatedEvent_spec
	{
		private Establish context_with_saga = () =>
		{
			_saga = new IndexerOrchestrationSaga();
			_instance = new IndexerOrchestrationSagaInstance(CombGuid.Generate());

			};

		private Because of = () => 
			_saga.RaiseEvent(_instance, x => x.MetaDataCreated);

		It should_be_in_state_IndexingPending = () =>
			_instance.CurrentState.ShouldEqual(_saga.IndexingPending);

		static IndexerOrchestrationSaga _saga;
		static IndexerOrchestrationSagaInstance _instance;
	}

	[Subject(typeof(IndexerOrchestrationSaga))]
	public class when_in_state_IndexingPending_and_recieve_event_IndexingStarted
		:WithFakes
	{
		Establish context_with_saga = () =>
		{
			_saga = new IndexerOrchestrationSaga();
			_instance = new IndexerOrchestrationSagaInstance(CombGuid.Generate());
			_instance.CurrentState = _saga.IndexingPending;
			_instance.Bus = An<IServiceBus>();
		};

		Because of = () =>
			_saga.RaiseEvent(_instance, x => x.IndexingStarted, 
			new TestIndexingStarted {CorrelationId = Guid.NewGuid()});

		It should_be_in_state_Indexing = () =>
			_instance.CurrentState.ShouldEqual(_saga.Indexing);

		static IndexerOrchestrationSaga _saga;
		static IndexerOrchestrationSagaInstance _instance;

		class TestIndexingStarted : Started
		{
			public Guid CorrelationId { get; set; }
		}
	}

	[Subject(typeof(IndexerOrchestrationSaga))]
	public class Indexing_saga_Indexing_spec
	{
		private Establish context_with_saga = () =>
		{
			_saga = new IndexerOrchestrationSaga();
			_instance = new IndexerOrchestrationSagaInstance(CombGuid.Generate());
			_instance.CurrentState = _saga.Indexing;
		};

		Because of = () =>
		    _saga.RaiseEvent(_instance, x => x.IndexingCompleted,
		                              new TestIndexingCompleted {CorrelationId = Guid.NewGuid()});

		It should_be_in_state_Final = () =>
			_instance.CurrentState.ShouldEqual(_saga.Final);

		static IndexerOrchestrationSaga _saga;
		static IndexerOrchestrationSagaInstance _instance;

		class TestIndexingCompleted : IndexingCompleted
		{
			public Guid CorrelationId { get; set; }
			public Guid DocumentId { get; set; }
		}
	}

	[Subject(typeof(IndexerOrchestrationSaga))]
	public class Indexing_saga_Indexing_got_timeout_spec
	{
		private Establish context_with_saga = () =>
		{
			_saga = new IndexerOrchestrationSaga();
			_instance = new IndexerOrchestrationSagaInstance(CombGuid.Generate());
			_instance.CurrentState = _saga.Indexing;
		};

		private Because of = () =>
			_saga.RaiseEvent(_instance, x => x.TimeoutExpired);

		It should_not_be_in_state_Final = () =>
			_instance.CurrentState.ShouldNotEqual(_saga.Final);

		static IndexerOrchestrationSaga _saga;
		static IndexerOrchestrationSagaInstance _instance;
	}
}

// ReSharper enable InconsistentNaming
#pragma warning restore 169
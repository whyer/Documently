using Automatonymous;
using Automatonymous.Impl;
using Documently.Messages.DocMetaEvents;
using Magnum;
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
			_instance = new Instance(CombGuid.Generate());
		};

		private Because of = () => 
			_saga.RaiseEvent(_instance, x => x.MetaDataCreated);

		It should_be_in_state_IndexingPending = () =>
			_instance.CurrentState.ShouldEqual(_saga.IndexingPending);

		static IndexerOrchestrationSaga _saga;
		static Instance _instance;
	}

	[Subject(typeof(IndexerOrchestrationSaga))]
	public class Indexing_saga_IndexingPending_spec
	{
		private Establish context_with_saga = () =>
		{
			_saga = new IndexerOrchestrationSaga();
			_instance = new Instance(CombGuid.Generate());
			_instance.CurrentState = _saga.IndexingPending;
		};

		private Because of = () =>
			_saga.RaiseEvent(_instance, x => x.IndexingStarted);

		It should_be_in_state_Indexing = () =>
			_instance.CurrentState.ShouldEqual(_saga.Indexing);

		static IndexerOrchestrationSaga _saga;
		static Instance _instance;
	}

	[Subject(typeof(IndexerOrchestrationSaga))]
	public class Indexing_saga_Indexing_spec
	{
		private Establish context_with_saga = () =>
		{
			_saga = new IndexerOrchestrationSaga();
			_instance = new Instance(CombGuid.Generate());
			_instance.CurrentState = _saga.Indexing;
		};

		private Because of = () =>
			_saga.RaiseEvent(_instance, x => x.IndexingCompleted);

		It should_be_in_state_Final = () =>
			_instance.CurrentState.ShouldEqual(_saga.Final);

		static IndexerOrchestrationSaga _saga;
		static Instance _instance;
	}

	[Subject(typeof(IndexerOrchestrationSaga))]
	public class Indexing_saga_Indexing_got_timeout_spec
	{
		private Establish context_with_saga = () =>
		{
			_saga = new IndexerOrchestrationSaga();
			_instance = new Instance(CombGuid.Generate());
			_instance.CurrentState = _saga.Indexing;
		};

		private Because of = () =>
			_saga.RaiseEvent(_instance, x => x.TimeoutExpired);

		It should_not_be_in_state_Final = () =>
			_instance.CurrentState.ShouldNotEqual(_saga.Final);

		static IndexerOrchestrationSaga _saga;
		static Instance _instance;
	}
}

// ReSharper enable InconsistentNaming
#pragma warning restore 169
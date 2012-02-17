#pragma warning disable 169
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
	public class Indexing_saga_spec_spec
	{
		private IndexerOrchestrationSaga subject;

		private Establish context = () =>
			{
			};

		private Because of = () =>
			{
			};

		// It should_have_5_in_its_name = () => subject. ...() assertions

	}
}

// ReSharper enable InconsistentNaming
#pragma warning restore 169
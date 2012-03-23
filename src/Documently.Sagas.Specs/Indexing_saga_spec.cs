using MassTransit.Testing;

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
	public class Indexing_saga_initially_spec
	{
		private Establish context_with_saga = () =>
			{
			};

		private Because of = () =>
			{
			};

		private static object _test;

		// It should_have_5_in_its_name = () => subject. ...() assertions

	}
}

// ReSharper enable InconsistentNaming
#pragma warning restore 169
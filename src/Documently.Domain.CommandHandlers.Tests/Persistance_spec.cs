using System;
using Documently.Domain.CommandHandlers.Tests.Framework;
using EventStore;
using EventStore.Persistence;
using FakeItEasy;
using Machine.Specifications;
using Documently.Domain.CommandHandlers.Infrastructure;
using Magnum.Policies;
using MassTransit;
using MassTransit.Util;
using It = Machine.Specifications.It;

#pragma warning disable 169
// ReSharper disable InconsistentNaming

namespace Documently.Domain.CommandHandlers.Tests
{
	static class RepoFac
	{
		public static readonly Func<DateTime> InstantGetter = () => DateTime.UtcNow;

		public static readonly ExceptionPolicy DefaultRepoExceptionPolicy = 
			ExceptionPolicy.InCaseOf<Exception>().Retry(1);

		public static EventStoreRepository CreateRepo(
			[NotNull] IStoreEvents eventStore = null,
			[NotNull] AggregateRootFactory factory = null,
			[NotNull] ExceptionPolicy retryPolicy = null)
		{
			return new EventStoreRepository(
				eventStore ?? A.Fake<IStoreEvents>(),
				factory ?? A.Fake<AggregateRootFactory>(),
				retryPolicy ?? DefaultRepoExceptionPolicy);
		}
	}

	class DummyAr : AggregateRoot, EventAccessor
	{
		readonly EventRouter _router;

		public DummyAr()
		{
			_router = EventRouter.For(this);
		}

		public NewId Id { get; set; }
		public uint Version { get; set; }

		public EventRouter Events
		{
			get { return _router; }
		}
	}

	// machine specifications howto:
	// https://github.com/machine/machine.specifications
	// sample: https://github.com/joliver/EventStore/blob/master/src/tests/EventStore.Core.UnitTests/OptimisticEventStreamTests.cs

	[Subject(typeof (EventStoreRepository))]
	public class when_getting_non_existent_entity_by_id
	{
		static NewId commitId;
		static EventStoreRepository subject;

		static IStoreEvents eventStore;
		static IEventStream stream;

		Establish context = () =>
			{
				commitId = NewId.Next();
				eventStore = A.Fake<IStoreEvents>(); 
				stream = A.Fake<IEventStream>();

				A.CallTo(() => eventStore.OpenStream(A<Guid>.Ignored, A<int>.Ignored, A<int>.Ignored))
					.Returns(stream);

				subject = RepoFac.CreateRepo(eventStore);
			};

		Because of = () => subject.Save(new DummyAr(), commitId, null);

		It should_have_called_eventStore_save =() => 
			A.CallTo(() => 
				stream.CommitChanges(A<Guid>.Ignored))
					.MustHaveHappened(Repeated.Exactly.Once);
	}

	[Subject(typeof(EventStoreRepository), "retry policy")]
	public class when_getting_storage_unavailble_exception
	{
		const int Retries = 3;

		static EventStoreRepository subject;

		static IEventStream stream;
		static IStoreEvents eventStore;

		Establish context = () =>
			{
				var retryPolicy = ExceptionPolicy.InCaseOf<StorageUnavailableException>().Retry(Retries);
				eventStore = A.Fake<IStoreEvents>();
				stream = A.Fake<IEventStream>();

				A.CallTo(() => eventStore.OpenStream(A<Guid>.Ignored, A<int>.Ignored, A<int>.Ignored))
					.Returns(stream);

				A.CallTo(() => stream.CommitChanges(A<Guid>.Ignored))
					.Throws(new StorageUnavailableException("oh noes"));

				subject = RepoFac.CreateRepo(eventStore, retryPolicy: retryPolicy);
			};

		Because of = () => Ignoring.Exception<StorageUnavailableException>(
			() => subject.Save(new DummyAr(), NewId.Next(), null));

		It should_open_stream = () =>
			A.CallTo(() =>
				eventStore.OpenStream(A<Guid>.Ignored, A<int>.Ignored, A<int>.Ignored))
					.MustHaveHappened();

		It should_retry = () =>
			A.CallTo(() => 
				stream.CommitChanges(A<Guid>.Ignored))
					.MustHaveHappened(Repeated.Exactly.Times(Retries + 1));
	}
}
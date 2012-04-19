using System;
using System.Collections;
using System.Collections.Generic;
using CommonDomain.Persistence;
using Documently.Domain;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Messages;
using Magnum.Reflection;
using MassTransit;
using MassTransit.Context;
using NUnit.Framework;
using System.Linq;

namespace Documently.Specs
{
	[TestFixture]
	public abstract class CommandTestFixture<TCommand, TCommandHandler, TAggregateRoot>
		where TCommand : class, Command
		where TCommandHandler : class
		where TAggregateRoot : class, AggregateRoot, EventAccessor
	{
		protected TAggregateRoot AggregateRoot;
		protected TCommandHandler CommandHandler;
		protected Exception CaughtException;
		protected IEnumerable<DomainEvent> PublishedEvents;
		protected IEnumerable<DomainEvent> PublishedEventsT { get { return PublishedEvents.Cast<DomainEvent>(); } } 
		protected FakeRepository Repository;
		protected virtual void SetupDependencies() { }

		protected T FirstOf<T>()
		{
			return PublishedEvents.Where(t => t.GetType() == typeof(T)).Cast<T>().First();
		}

		protected virtual IEnumerable<DomainEvent> Given()
		{
			return new List<DomainEvent>();
		}

		protected virtual void Finally() { }

		protected abstract TCommand When();

		protected abstract void Consume(ConsumeContext<TCommand> cmd);

		[SetUp]
		public void Setup()
		{
			AggregateRoot = FastActivator.Create(typeof(TAggregateRoot)) as TAggregateRoot;
			Repository = new FakeRepository(AggregateRoot);
			CaughtException = new ThereWasNoExceptionButOneWasExpectedException();

			foreach (var evt in Given())
			{
				AggregateRoot.Events.ApplyEvent(evt);
			}

			CommandHandler = BuildCommandHandler();
			SetupDependencies();
			try
			{
				var cmd = When();
				var ctx = new ConsumeContext<TCommand>(ReceiveContext.Empty(), cmd);
				Consume(ctx);
				if (Repository.SavedAggregate == null)
					PublishedEvents = AggregateRoot.Events.GetUncommitted();
				else
					PublishedEvents = Repository.SavedAggregate.Events.GetUncommitted();
			}
			catch (Exception exception)
			{
				CaughtException = exception;
			}
			finally
			{
				Finally();
			}
		}

		private TCommandHandler BuildCommandHandler()
		{
		    Func<DomainRepository> createReposFunc = () => Repository;
			return Activator.CreateInstance(typeof(TCommandHandler), createReposFunc) as TCommandHandler;
		}
	}

	public class ThereWasNoExceptionButOneWasExpectedException : Exception { }
}
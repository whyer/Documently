using System;
using System.Linq;
using Documently.Commands;
using Documently.Domain.Events;
using NUnit.Framework;

namespace CQRSSample.Specs.Commands
{
	public class All_commands_and_events_must_have_default_ctor
	{
		[Test]
		public void verify_commands()
		{
			var commands = typeof(Command).Assembly.GetExportedTypes().Where(x => x.BaseType == typeof(Command)).ToList();
			foreach (var command in commands)
			{
				if (command.GetConstructors()
					.Any(c => c.GetParameters().Count() == 0)) continue;
				Assert.Fail(command + " doesn't have a default c'tor.");
			}
		}

		[Test]
		public void verify_events()
		{
			var faultyEvents = typeof(DomainEvent).Assembly
				.GetExportedTypes()
				.Where(x => x.BaseType == typeof(DomainEvent))
				.Where(command => command.GetConstructors().Any(c => c.GetParameters().Count() == 0) == false)
				.ToList();
			
			if (faultyEvents.Count > 0)
				Assert.Fail("All events of:{0}{1}{0}have no empty c'tors", Environment.NewLine, string.Join(",", faultyEvents));
			
		}
	}
}
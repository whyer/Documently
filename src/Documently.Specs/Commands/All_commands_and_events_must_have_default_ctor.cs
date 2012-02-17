using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Documently.Commands;
using Documently.Messages;
using NUnit.Framework;

namespace Documently.Specs.Commands
{
	[TestFixture]
	public class all_cmds_and_events_spec
	{
		private static void for_all_types(Type t, Action<Type> asserter)
		{
			foreach (var type in t.Assembly.GetExportedTypes().Where(t1 => t1.BaseType == t))
				asserter(type);
		}

		[TestCase(typeof(Command))]
		[TestCase(typeof(DomainEvent))]
		public void should_have_a_default_protected_ctor(Type t)
		{
			for_all_types(t, type => Assert.That(
				HasDefaultProtectedCtor(type) || HasSinglePublicCtor(type),
				string.Format(
					"event/command type {0} does not contain a default protected constructor (but it might have a public one)",
					type.Name)));
		}

		private static bool HasDefaultProtectedCtor(Type type)
		{
			return type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).Any(c => c.GetParameters().Count() == 0);
		}

		private static bool HasSinglePublicCtor(Type type)
		{
			return
				type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).All(c => c.GetParameters().Count() == 0)
					&& type.GetConstructors().Count() == 1;
		}

		[TestCase(typeof(Command))]
		[TestCase(typeof(DomainEvent))]
		public void should_have_obsolete_defined_on_that_ctor(Type t)
		{
			for_all_types(t, type =>
				Assert.That(
					type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
						.Any(ctor => ctor.GetCustomAttributes(typeof(ObsoleteAttribute), false).Length > 0)
					|| HasSinglePublicCtor(type),
					string.Format("event/command type {0} does not declare obsolete on default protected constructor",
						type.Name)));
		}

		[TestCase(typeof(Command))]
		[TestCase(typeof(DomainEvent))]
		public void should_have_protected_setters(Type t)
		{
			for_all_types(t, type =>
			{
				var invalids = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
					.Where(p => !p.CanWrite ||
						(p.GetSetMethod(true /* non public set method */).IsPrivate
						|| p.GetSetMethod(true).IsPublic))

					.Where(p => new HashSet<string> { "CorrelationId",  "AggregateId", "Version" }.Contains(p.Name) == false) // ignore correlation id
					.Where(p => !p.DeclaringType.Name.Contains("Customer"))
					.ToList();
				Assert.That(invalids.Count, Is.EqualTo(0),
					string.Format("Properties {{ {0} }} on event {1} don't have proptected setters on public properties.",
					string.Join(",", invalids.Select(p => p.Name)), type.Name));
			});
		}

		[TestCase(typeof(Command))]
		[TestCase(typeof(DomainEvent))]
		public void should_be_serializable(Type t)
		{
			for_all_types(t, type =>
				Assert.That(type.IsSerializable,
					string.Format("event/command {0} is not serializable, but should be!", type.Name)));
		}
	}
}
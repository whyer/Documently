using System;
using MassTransit.Util;

namespace Documently.Domain.CommandHandlers.Tests
{
	static class Ignoring
	{
		public static void Exception<T>([NotNull] this Action a)
			where T:Exception
		{
			if (a == null) throw new ArgumentNullException("a");
			try
			{
				a();
			}
			catch (Exception e)
			{
				if (!(e is T))
					throw;
			}
		}
	}
}
using System;

namespace Documently.Commands
{
	public abstract class Command
	{
		public readonly Guid Id;
		public readonly int Version;

		protected Command(Guid id)
		{
			Id = id;
		}

		protected Command(Guid id, int version)
		{
			Id = id;
			Version = version;
		}
	}
}
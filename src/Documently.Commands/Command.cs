using System;

namespace Documently.Commands
{
	public abstract class Command
	{
		public Guid Id { get; protected set; }
		public int Version { get; protected set; }

		protected Command()
		{
		}

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
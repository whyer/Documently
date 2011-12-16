using System;

namespace Documently.Commands
{
	public abstract class Command
	{
		public Guid ArId { get; protected set; }
		public int Version { get; protected set; }

		protected Command()
		{
		}

		protected Command(Guid arId)
		{
			ArId = arId;
		}

		protected Command(Guid arId, int version)
		{
			ArId = arId;
			Version = version;
		}
	}
}
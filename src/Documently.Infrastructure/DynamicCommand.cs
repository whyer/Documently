using System.Dynamic;
using Documently.Messages;

namespace Documently.Infrastructure
{
	public class DynamicCommand<T> : DynamicObject where T : Command
	{
		private readonly dynamic _innerCommand;

		public DynamicCommand(T command)
		{
			_innerCommand = command;
		}

		public T InnerCommand
		{
			get { return _innerCommand; }
		}
	}
}
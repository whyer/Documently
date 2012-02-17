using System;
using Documently.Commands;
using Documently.Messages;

namespace Documently.Infrastructure
{
	public interface IBus
	{
		void Send<T>(T command) where T : Command;
		void RegisterHandler<T>(Action<T> handler) where T : DomainEvent;
	}
}
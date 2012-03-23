using System;
using Documently.Commands;
using Documently.Messages;

namespace Documently.Infrastructure
{
	public interface IBus
	{
		void Send<T>(T command) where T : class, Command;
		void RegisterHandler<T>(Action<T> handler) where T : class, DomainEvent;
	}
}
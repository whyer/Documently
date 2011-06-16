using System;
using CQRSSample.Domain.Events;
using Documently.Commands;

namespace CQRSSample.Infrastructure
{
	public interface IBus
	{
		void Send<T>(T command) where T : Command;
		void RegisterHandler<T>(Action<T> handler) where T : DomainEvent;
	}
}
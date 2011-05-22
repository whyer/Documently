using System;
using CQRSSample.Commands;
using CQRSSample.Domain.Events;

namespace CQRSSample.Infrastructure
{
	public interface IBus
	{
		void Send<T>(T command) where T : Command;
		void RegisterHandler<T>(Action<T> handler) where T : DomainEvent;
	}
}
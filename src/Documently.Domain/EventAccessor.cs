using System.Collections.Generic;
using Documently.Messages;
using MassTransit.Util;

namespace Documently.Domain
{
	public interface EventAccessor
	{
		EventRouter Events { get; }
	}
}
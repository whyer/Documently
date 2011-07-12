using System;

namespace Documently.Domain.Events
{
	[Serializable]
	public class DocumentIndexed : DomainEvent
	{
		public DocumentIndexed()
		{
		}

		public DocumentIndexed(Guid arId, uint arVersion) : base(arId, arVersion)
		{
		}
	}
}
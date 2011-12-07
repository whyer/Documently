using System;

namespace Documently.Domain.Events
{
	//public interface IDomainEvent
	//{
	//    Guid AggregateId { get; set; }
	//    int Version { get; set; }
	//}

	/// <summary>
	/// Denotes an event in the domain model.
	/// </summary>
	[Serializable]
	public abstract class DomainEvent
	{
		protected DomainEvent()
		{
		}

		protected DomainEvent(Guid aggregateId, int aggregateVersion)
		{
			AggregateId = aggregateId;
			Version = aggregateVersion;
		}

		public Guid AggregateId { get; protected set; }

		/// <summary>
		/// Gets the version of the aggregate which this event corresponds to.
		/// E.g. CreateNewCustomerCommand would map to (:NewCustomerCreated).Version = 1,
		/// as that event corresponds to the creation of the customer.
		/// </summary>
		public int Version { get; protected set; }
	}
}
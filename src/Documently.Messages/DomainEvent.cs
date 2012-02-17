using System;

namespace Documently.Messages
{
	/// <summary>
	/// Denotes an event in the domain model.
	/// </summary>
	public interface DomainEvent
	{
		/// <summary>
		/// Gets the aggregate root id of the domain event.
		/// </summary>
		Guid AggregateId { get; }

		/// <summary>
		/// Gets the version of the aggregate which this event corresponds to.
		/// E.g. CreateNewCustomerCommand would map to (:NewCustomerCreated).Version = 1,
		/// as that event corresponds to the creation of the customer.
		/// </summary>
		int Version { get; set; }
	}
}
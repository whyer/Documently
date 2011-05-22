using System;
using System.Collections.Generic;
using CQRSSample.Commands;
using CQRSSample.Domain.CommandHandlers;
using CQRSSample.Domain.Domain;
using CQRSSample.Domain.Events;
using NUnit.Framework;

namespace CQRSSample.Specs.CustomerSpecs
{
	public class when_non_existing_customer_is_relocating :
		CommandTestFixture<RelocateCustomerCommand, RelocatingCustomerCommandHandler, Customer>
	{
		protected override IEnumerable<DomainEvent> Given()
		{
			yield break;
		}

		protected override RelocateCustomerCommand When()
		{
			return new RelocateCustomerCommand(Guid.NewGuid(), "Ringstraße", "1a", "1010", "Wien");
		}

		[Test]
		public void should_throw_non_existing_customer_event()
		{
			Assert.IsInstanceOfType(typeof (NonExistingCustomerException), CaughtException);
		}
	}
}
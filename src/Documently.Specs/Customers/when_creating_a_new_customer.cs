using System;
using Documently.Commands;
using Documently.Domain.CommandHandlers;
using Documently.Domain.Domain;
using Documently.Domain.Events;
using NUnit.Framework;
using SharpTestsEx;

namespace CQRSSample.Specs.Customers
{
	public class when_creating_a_new_customer :
		CommandTestFixture<CreateNewCustomer, CreateCustomerCommandHandler, Customer>
	{
		protected override CreateNewCustomer When()
		{
			return new CreateNewCustomer(Guid.NewGuid(), "Jörg Egretzberger", "Ringstraße", "1", "1010", "Wien", "01/123456");
		}

		[Test]
		public void Then_a_client_created_event_will_be_published()
		{
			Assert.AreEqual(typeof (CustomerCreatedEvent), PublishedEvents.Last().GetType());
		}

		[Test]
		public void Then_the_published_event_will_contain_the_name_of_the_client()
		{
			Assert.That(PublishedEvents.Last<CustomerCreatedEvent>().CustomerName == "Jörg Egretzberger");
		}

		[Test]
		public void Then_the_published_event_will_contain_the_address_of_the_client()
		{
			PublishedEvents.Last<CustomerCreatedEvent>().Street.Should().Be.EqualTo("Ringstraße");
			PublishedEvents.Last<CustomerCreatedEvent>().StreetNumber.Should().Be.EqualTo("1");
			PublishedEvents.Last<CustomerCreatedEvent>().PostalCode.Should().Be.EqualTo("1010");
			PublishedEvents.Last<CustomerCreatedEvent>().City.Should().Be.EqualTo("Wien");
		}

		[Test]
		public void Then_the_published_event_will_contain_the_phone_number_of_the_client()
		{
			PublishedEvents.Last<CustomerCreatedEvent>().PhoneNumber.Should().Be.EqualTo("01/123456");
		}
	}
}
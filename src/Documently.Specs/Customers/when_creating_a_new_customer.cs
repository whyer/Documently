// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using Documently.Domain;
using Documently.Domain.CommandHandlers.ForCustomer;
using Documently.Messages.CustCommands;
using Documently.Messages.CustEvents;
using MassTransit;
using NUnit.Framework;
using SharpTestsEx;
using Address = Documently.Messages.CustCommands.Address;

namespace Documently.Specs.Customers
{
	class RegisterNewImpl : RegisterNew
	{
		public NewId AggregateId { get; set; }
		public uint Version { get; set; }
		public string CustomerName { get; set; }
		public string PhoneNumber { get; set; }
		public Address Address { get; set; }
	}

	class AddressImpl : Address
	{
		public string Street { get; set; }
		public string StreetNumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
	}

	public class when_creating_a_new_customer :
		CommandTestFixture<RegisterNew, CreateCustomerCommandHandler, Customer>
	{
		protected override RegisterNew When()
		{
			return new RegisterNewImpl
				{
					AggregateId = NewId.Next(),
					CustomerName = "Jörg Egretzberger",
					Address = new AddressImpl
						{
							Street = "Ringstraße",
							StreetNumber = "1",
							PostalCode = "1010",
							City = "Wien"
						},
					PhoneNumber = "01/123456"
				};
		}

		[Test]
		public void Then_a_client_created_event_will_be_published()
		{
			Assert.AreEqual(typeof (Created), PublishedEvents.Last().GetType());
		}

		[Test]
		public void Then_the_published_event_will_contain_the_name_of_the_client()
		{
			Assert.That(PublishedEvents.Last<Created>().CustomerName == "Jörg Egretzberger");
		}

		[Test]
		public void Then_the_published_event_will_contain_the_address_of_the_client()
		{
			PublishedEvents.Last<Created>().Street.Should().Be.EqualTo("Ringstraße");
			PublishedEvents.Last<Created>().StreetNumber.Should().Be.EqualTo("1");
			PublishedEvents.Last<Created>().PostalCode.Should().Be.EqualTo("1010");
			PublishedEvents.Last<Created>().City.Should().Be.EqualTo("Wien");
		}

		[Test]
		public void Then_the_published_event_will_contain_the_phone_number_of_the_client()
		{
			PublishedEvents.Last<Created>().PhoneNumber.Should().Be.EqualTo("01/123456");
		}
	}
}
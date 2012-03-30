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

using Documently.Messages.CustEvents;
using Magnum.Reflection;
using NUnit.Framework;
using Machine.Specifications;

namespace Documently.Domain.CommandHandlers.Tests.Assumptions
{
	public class CanCreateProxy
	{
		[Test]
		public Messages.CustDtos.Address can_create_object_proxy()
		{
			var a = InterfaceImplementationExtensions.InitializeProxy<Messages.CustDtos.Address>(new
				{
					Street = "A",
					StreetNumber = 57u,
					PostalCode = "45555",
					City = "Dubai"
				});

			a.ShouldNotBeNull();

			a.Street.ShouldEqual("A");
			a.StreetNumber.ShouldEqual(57u);
			a.PostalCode.ShouldEqual("45555");
			a.City.ShouldEqual("Dubai");

			return a;
		}

		[Test]
		public void can_create_proxy_object_with_interface_property()
		{
			var r = InterfaceImplementationExtensions.InitializeProxy<Registered>(new
				{
					CustomerName = "haf",
					Address = can_create_object_proxy(),
					PhoneNumber = "+4686676767"
				});

			r.ShouldNotBeNull();

			r.Address.Street.ShouldEqual("A");
		}
	}
}
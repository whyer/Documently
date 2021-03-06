﻿// Copyright 2011 Chris Patterson, Dru Sellers
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

using MassTransit;
using MassTransit.Advanced;
using Magnum.Extensions;
using MassTransit.SubscriptionConfigurators;
using NUnit.Framework;

namespace Documently.Sagas.Specs
{
	[TestFixture]
	public abstract class MassTransitTestFixture
	{
		IServiceBus _bus;

		protected IServiceBus Bus
		{
			get { return _bus; }
		}

		[TestFixtureSetUp]
		public void Setup()
		{
			_bus = ServiceBusFactory.New(x =>
			{
				x.ReceiveFrom("loopback://localhost/bus");
				x.UseJsonSerializer();
				x.SetReceiveTimeout(50.Milliseconds());

				x.Subscribe(ConfigureSubscriptions);
			});
		}

		protected virtual void ConfigureSubscriptions(SubscriptionBusServiceConfigurator configurator)
		{
		}

		[TestFixtureTearDown]
		public void Teardown()
		{
			_bus.Dispose();
		}
	}


	[TestFixture]
	public class Doing_nothing :
		MassTransitTestFixture
	{
		[Test]
		public void Should_setup_the_bus_integration()
		{
		}
	}
}
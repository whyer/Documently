using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Domain.Events;
using Documently.Infrastructure;
using Documently.ReadModel;
using NUnit.Framework;
using SharpTestsEx;

namespace CQRSSample.Specs
{
	public class windsor_reg_specs
	{
		[Test]
		public void registers_all_views()
		{
			var c = new WindsorContainer();
			c.Register(Component.For<IBus>().ImplementedBy<InProcessBus>());
			RegisterEventHandlersInBus.BootStrap(c);

			c.ResolveAll<HandlesEvent<CustomerCreatedEvent>>()
				.Any(x => x.GetType().Equals(typeof(CustomerAddressView)))
				.Should(" the address view should be in the enumerable of those handlers").Be.True();
		} 
	}
}
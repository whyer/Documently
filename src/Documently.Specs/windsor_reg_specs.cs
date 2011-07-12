using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Domain.Events;
using Documently.Infrastructure;
using Documently.ReadModel;
using MassTransit;
using Moq;
using NUnit.Framework;
using Raven.Client;
using SharpTestsEx;

namespace CQRSSample.Specs
{
	public class windsor_reg_specs
	{
		[Test]
		public void registers_all_views()
		{
			var c = new WindsorContainer();

			c.Register(
				Component.For<IDocumentStore>().Instance(new Mock<IDocumentStore>().Object),
				Component.For<IWindsorContainer>().Instance(c),
				Component.For<IBus>().ImplementedBy<InProcessBus>(),
				AllTypes.FromAssembly(typeof(CustomerListView).Assembly)
					.BasedOn(typeof(Consumes<>.All))
					.WithService.FromInterface(typeof(Consumes<>.All)));

			c.ResolveAll<Consumes<CustomerCreatedEvent>.All>()
				.Any(x => x.GetType().Equals(typeof(CustomerAddressView)))
				.Should(" the address view should be in the enumerable of those handlers").Be.True();
		} 
	}
}
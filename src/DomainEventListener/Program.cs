using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Domain.Events;
using Documently.Infrastructure;
using Documently.Infrastructure.Installers;

namespace DomainEventListener
{
    class Program
    {
        private static IWindsorContainer _Container;

        static void Main(string[] args)
        {
            _Container = new WindsorContainer().Install(new BusInstaller("rabbitmq://localhost/Documently.App"), new EventStoreInstaller());
            _Container.Register(Component.For<IWindsorContainer>().Instance(_Container));
            var bus = _Container.Resolve<IBus>();
            bus.RegisterHandler<CustomerCreatedEvent>(ev => Console.WriteLine(ev.CustomerName));
            Console.ReadKey(true);
        }
    }
}

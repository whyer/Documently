using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Documently.Domain.CommandHandlers.ForCustomer;
using MassTransit;

namespace Documently.Infrastructure.Installers
{
	public class CommandHandlerInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.AddFacility<TypedFactoryFacility>();

			container.Register(
				AllTypes.FromAssemblyContaining(typeof(RegisterNewHandler))
				.Where(x => x.GetInterface(typeof (Consumes<>.All).Name) != null));
		}
	}
}
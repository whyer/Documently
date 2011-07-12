using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Documently.Infrastructure;
using Documently.Infrastructure.Installers;
using Raven.Client.Document;

namespace Documently.WpfClient
{
	public class WpfClientBootstrapper : Bootstrapper<ShellViewModel>
	{
		private IWindsorContainer _Container;

		protected override void Configure()
		{
			var viewStore = new DocumentStore { ConnectionStringName = Keys.RavenDbConnectionStringName };
			viewStore.Initialize();

			_Container = Keys.BootStrap(viewStore, "rabbitmq://localhost/Documently.WpfClient");
			// adds and configures all components using WindsorInstallers from executing assembly
			
			_Container.Install(FromAssembly.This());
			_Container.Install(new ReadRepositoryInstaller());
		}

		protected override object GetInstance(Type service, string key)
		{
			return string.IsNullOrWhiteSpace(key) 
				? _Container.Resolve(service) 
				: _Container.Resolve(key, service);
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return (IEnumerable<object>)_Container.ResolveAll(service);
		}

		protected override void BuildUp(object instance)
		{
			_Container.BuildUp(instance);
		}

		protected override void OnExit(object sender, EventArgs e)
		{
			_Container.Dispose();
		}
	}

	public static class WindsorExtensions
	{
		public static void BuildUp(this IWindsorContainer container, object instance)
		{
			instance.GetType().GetProperties()
				 .Where(property => property.CanWrite && property.PropertyType.IsPublic)
				 .Where(property => container.Kernel.HasComponent(property.PropertyType))
				 .ForEach(property => property.SetValue(instance, container.Resolve(property.PropertyType), null));
		}
	}
}
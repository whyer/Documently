using Documently.WebApp.Handlers;
using FubuMVC.Core;
using FubuMVC.Spark;

namespace Documently.WebApp
{
    public class ConfigureFubuMVC : FubuRegistry
    {
        public ConfigureFubuMVC()
        {
            // This line turns on the basic diagnostics and request tracing
            IncludeDiagnostics(true);

            ApplyHandlerConventions<HandlersMarker>();

            // Policies
            //Routes.HomeIs<Documently.WebApp.Handlers.Home.GetHandler>(x => x.Execute());
            // Policies
            Routes
                .IgnoreControllerNamesEntirely()
                .IgnoreMethodSuffix("Html")
                .RootAtAssemblyNamespace()
				.HomeIs<Handlers.Home.GetHandler>(x => x.Execute());

            this.UseSpark();

            // Match views to action methods by matching
            // on model type, view name, and namespace
            Views.TryToAttachWithDefaultConventions();
        }
    }
}
using Documently.Infrastructure.Installers;

namespace Documently.Infrastructure
{
	/// <summary>
	/// Helper class that creates the windsor container.
	/// </summary>
	public static class Keys
	{
		public static readonly string RavenDbConnectionStringName = "RavenDB";
		public static readonly string DomainServiceEndpoint = "rabbitmq://localhost/Documently.Domain.Service";
	}
}
using Documently.Messages.CustCommands;
using MassTransit;

namespace Documently.WpfClient.Modules.CustomerDetails.CreateCustomer
{
	public class RegisterNewImpl : RegisterNew
	{
		public NewId AggregateId { get; set; }
		public uint Version { get; set; }
		public string CustomerName { get; set; }
		public string PhoneNumber { get; set; }
		public Address Address { get; set; }
	}
}
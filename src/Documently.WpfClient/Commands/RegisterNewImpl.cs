using System;
using Documently.Messages.CustCommands;
using Documently.Messages.CustDtos;
using MassTransit;

namespace Documently.WpfClient.Commands
{
	public class RegisterNewImpl : RegisterNew
	{
		public Guid AggregateId { get; set; }
		public uint Version { get; set; }
		public string CustomerName { get; set; }
		public string PhoneNumber { get; set; }
		public Address Address { get; set; }
	}
}
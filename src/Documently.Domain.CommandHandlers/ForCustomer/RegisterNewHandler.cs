using System;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Messages.CustCommands;
using MassTransit;
using System.Linq;

namespace Documently.Domain.CommandHandlers.ForCustomer
{
	public class RegisterNewHandler : Consumes<IConsumeContext<RegisterNew>>.All
	{
		private readonly Func<DomainRepository> _Repository;

		public RegisterNewHandler(Func<DomainRepository> repository)
		{
			_Repository = repository;
		}

		public void Consume(IConsumeContext<RegisterNew> context)
		{
			var repo = _Repository();
			var command = context.Message;

			var client = Customer.CreateNew(command.AggregateId, new CustomerName(command.CustomerName),
			                                new Address(command.Address.Street, command.Address.StreetNumber,
			                                            command.Address.PostalCode, command.Address.City),
			                                new PhoneNumber(command.PhoneNumber));

			repo.Save(client, new Guid(context.MessageId).ToNewId(), context.Headers.ToDictionary(x => x.Key, x => x.Value));
		}
	}
}
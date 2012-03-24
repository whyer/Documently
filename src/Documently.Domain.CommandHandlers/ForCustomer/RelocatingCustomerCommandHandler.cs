using System;
using Documently.Commands;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Messages.CustCommands;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public class RelocatingCustomerCommandHandler : Consumes<RelocateTheCustomer>.All
	{
		private readonly Func<DomainRepository> _repository;

		public RelocatingCustomerCommandHandler(Func<DomainRepository> repository)
		{
			_repository = repository;
		}

		public void Consume(RelocateTheCustomer command)
		{
			var repo = _repository();
			var address = command.NewAddress;
			var customer = repo.GetById<Customer>(command.AggregateId, command.Version);
			customer.RelocateCustomer(address.Street, address.StreetNumber, address.PostalCode, address.City);
			repo.Save(customer, NewId.Next(), null);
		}
	}
}
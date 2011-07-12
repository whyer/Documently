using System;
using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using MassTransit;

namespace Documently.Domain.CommandHandlers
{
	public class RelocatingCustomerCommandHandler : Consumes<RelocateTheCustomer>.All
	{
		private readonly Func<IRepository> _repository;

		public RelocatingCustomerCommandHandler(Func<IRepository> repository)
		{
			_repository = repository;
		}

		public void Consume(RelocateTheCustomer command)
		{
			var repo = _repository();
			const int version = 0;
			var customer = repo.GetById<Customer>(command.Id, version);
			customer.RelocateCustomer(command.Street, command.Streetnumber, command.PostalCode, command.City);
			repo.Save(customer, Guid.NewGuid(), null);
		}
	}
}
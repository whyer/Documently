using CommonDomain.Persistence;
using Documently.Commands;
using Documently.Domain.Domain;
using Magnum;

namespace Documently.Domain.CommandHandlers
{
	public class CreateCustomerCommandHandler : Handles<CreateNewCustomer>
	{
		private readonly IRepository _Repository;

		public CreateCustomerCommandHandler(IRepository repository)
		{
			_Repository = repository;
		}

		public void Consume(CreateNewCustomer command)
		{
			var client = Customer.CreateNew(command.Id, new CustomerName(command.CustomerName),
			                                new Address(command.Street, command.StreetNumber,
			                                            command.PostalCode, command.City),
			                                new PhoneNumber(command.PhoneNumber));

			_Repository.Save(client, CombGuid.Generate(), null);
		}
	}
}
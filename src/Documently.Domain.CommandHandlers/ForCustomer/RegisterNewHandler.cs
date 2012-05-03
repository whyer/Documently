// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using System;
using Documently.Domain.CommandHandlers.Infrastructure;
using Documently.Messages.CustCommands;
using MassTransit;
using MassTransit.Util;

namespace Documently.Domain.CommandHandlers.ForCustomer
{
	public class RegisterNewHandler : Consumes<IConsumeContext<RegisterNew>>.All
	{
		private readonly Func<DomainRepository> _repository;

		public RegisterNewHandler([NotNull] Func<DomainRepository> repository)
		{
			if (repository == null) throw new ArgumentNullException("repository");
			_repository = repository;
		}

		public void Consume(IConsumeContext<RegisterNew> context)
		{
			var command = context.Message;

			var client = Customer.CreateNew(command.AggregateId, new CustomerName(command.CustomerName),
			                                new Address(command.Address.Street, command.Address.StreetNumber,
			                                            command.Address.PostalCode, command.Address.City),
			                                new PhoneNumber(command.PhoneNumber));

			var repo = _repository();
			repo.Save(client, context.GetMessageId(), context.GetHeaders());
		}
	}
}
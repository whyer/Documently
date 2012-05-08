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
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers.ForCustomer
{
	public class RelocateTheCustomerHandler : Consumes<RelocateTheCustomer>.Context
	{
		readonly Func<DomainRepository> _repository;

		public RelocateTheCustomerHandler(Func<DomainRepository> repository)
		{
			_repository = repository;
		}

		public void Consume(IConsumeContext<RelocateTheCustomer> message)
		{
			var repo = _repository();
			var address = message.Message.NewAddress;
			var customer = repo.GetById<Customer>(message.Message.AggregateId, message.Message.Version);
			customer.RelocateCustomer(address.Street, address.StreetNumber, address.PostalCode, address.City);
			repo.Save(customer, CombGuid.Generate(), null);
		}
	}
}
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
using Documently.Commands.DocumentMetaData;
using Documently.Domain.CommandHandlers.Infrastructure;
using Magnum;
using MassTransit;

namespace Documently.Domain.CommandHandlers.ForDocMeta
{
	public class AssociateWithCollectionHandler : Consumes<AssociateWithCollection>.All
	{
		private readonly Func<DomainRepository> _repository;

		public AssociateWithCollectionHandler(Func<DomainRepository> repository)
		{
			if (repository == null) throw new ArgumentNullException("repository");
			_repository = repository;
		}

		public void Consume(AssociateWithCollection message)
		{
			var repository = _repository();
			var document = repository.GetById<DocumentMetaData>(message.AggregateId);

			document.AssociateWithCollection(message.CollectionId);
			repository.Save(document, CombGuid.Generate(), null);
		}
	}
}
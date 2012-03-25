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
using Documently.Messages.DocCollectionCmds;
using MassTransit;

namespace Documently.Domain.CommandHandlers.ForDocCollection
{
	public class CreateHandler : Consumes<IConsumeContext<Create>>.All
	{
		private readonly Func<DomainRepository> _reposistory;

		public CreateHandler(Func<DomainRepository> reposistory)
		{
			if (reposistory == null)
				throw new ArgumentNullException("reposistory");
			_reposistory = reposistory;
		}

		public void Consume(IConsumeContext<Create> context)
		{
			var collection = DocumentCollection.CreateNew(context.Message.Name);
			var repository = _reposistory();
			repository.Save(collection, new NewId(context.MessageId),
			                context.GetHeaders());
		}
	}
}
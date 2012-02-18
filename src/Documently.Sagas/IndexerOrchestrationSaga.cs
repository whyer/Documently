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
using Automatonymous;
using Documently.Messages;
using Documently.Messages.DocumentMetaData;
using Documently.Messages.Indexer;
using MassTransit;
using MassTransit.Services.Timeout.Messages;
using Created = Documently.Messages.DocumentMetaData.Created;

namespace Documently.Sagas
{
	public class Instance
		: SagaStateMachineInstance
	{
		public State CurrentState { get; set; }
		public Guid CorrelationId { get; set; }
		public IServiceBus Bus { get; set; }
	}

	public class IndexerOrchestrationSaga
		: AutomatonymousStateMachine<Instance>
	{
		public IndexerOrchestrationSaga()
		{
			State(() => Indexing);
			State(() => IndexingCancelled);
			State(() => IndexingPending);

			Event(() => MetaDataCreated);
			Event(() => IndexingStarted);
			Event(() => IndexingCompleted);

			During(Initial,
				When(MetaDataCreated)
					.TransitionTo(IndexingPending));

			During(IndexingPending,
				When(IndexingStarted)
					.Then(instance => Bus.Publish(new ScheduleTimeout(CorrelationId, DateTime.UtcNow.AddMinutes(2))))
					.TransitionTo(Indexing));

			During(Indexing,
				When(IndexingCompleted)
					.TransitionTo(Final),
				When(TimeoutExpired)
					.Then((_) => Bus.Publish<IndexingTakingTooLong>(new
						{
							CorrelationId
						}))
				);
		}

		public State IndexingPending { get; private set; }
		public State Indexing { get; private set; }
		public State IndexingCancelled { get; private set; }

		// uploads actual doc:
		// [C] CreateDocumentMetaData -> 
		// [E] DocumentMetaDataCreated (2 listeners; Saga and Indexer)
		public Event<Created> MetaDataCreated { get; private set; }

		// [E] Indexer.Started
		public Event<Started> IndexingStarted { get; private set; }
		
		// [E] Indexer.IndexingComplete
		public Event<IndexingCompleted> IndexingCompleted { get; private set; }

		// meanwhile...
		public Event<TimeoutExpired> TimeoutExpired { get; private set; }
	}
}
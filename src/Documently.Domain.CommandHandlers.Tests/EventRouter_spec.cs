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

using System.Collections.Generic;
using Documently.Messages;
using Machine.Specifications;
using MassTransit;

namespace Documently.Domain.CommandHandlers.Tests
{
	class DaBoss
		: AggregateRoot, EventAccessor
	{
		readonly List<string> _bossMemory = new List<string>();

		public DaBoss()
		{
			// sut
			_events = EventRouter.For(this);
			
			// invariants
			Id = NewId.Next();
			Version = 1;
		}

		public NewId Id { get; set; }
		public uint Version { get; set; }

		// public method, causes state change
		public void Speak(string nuggetOfWisdom)
		{
			this.Raise<DaBoss, UtteredWisdom>(
				new Wisdom
					{
						BossSayz = nuggetOfWisdom,
						Version = Version + 1,
						AggregateId = Id
					});
		}

		// implements EventAccessor, infrastructure
		readonly EventRouter _events;

		EventRouter EventAccessor.Events
		{
			get { return _events; }
		}

		// applying a state change
		private void Apply(UtteredWisdom statement)
		{
			_bossMemory.Add(statement.BossSayz);
		}

		// for asserting on
		public IEnumerable<string> BossQuotes
		{
			get { return _bossMemory; }
		}
	}

	class Wisdom
		: UtteredWisdom
	{
		public string BossSayz { get; set; }
		public NewId AggregateId { get; set; }
		public uint Version { get; set; }
	}

	interface UtteredWisdom
		: DomainEvent
	{
		string BossSayz { get; }
	}

	[Subject(typeof(EventRouter))]
	public class When_raising_event_on_object_instance_spec
	{
		static string _wisdom = "When you don't know what to do, walk fast and look worried.";
		
		static DaBoss daBuzz;

		Establish context = () =>
			{
				daBuzz = new DaBoss();
			};

		Because of = () => 
			daBuzz.Speak(_wisdom);

		It should_contain_corresponding_applied_event = () => 
			daBuzz.BossQuotes.ShouldContain(_wisdom);
	}
}
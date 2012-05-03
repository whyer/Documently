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
using Documently.Messages;

namespace Documently.Infrastructure.Misc
{
	public class EventDescriptor
	{
		public readonly DomainEvent EventData;
		public readonly Guid Id;
		public readonly int Version;

		public EventDescriptor(Guid aggregateId, DomainEvent eventData, int version)
		{
			EventData = eventData;
			Version = version;
			Id = aggregateId;
		}
	}
}
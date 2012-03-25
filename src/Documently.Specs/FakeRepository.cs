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
using System.Collections.Generic;
using Documently.Domain;
using Documently.Domain.CommandHandlers.Infrastructure;
using MassTransit;

namespace Documently.Specs
{
	public class FakeRepository : DomainRepository
	{
		private readonly object _instance;
		public AggregateRoot SavedAggregate { get; set; }

		public FakeRepository(object instance)
		{
			_instance = instance;
		}

		public T GetById<T>(NewId aggregateId, uint version) where T : class, AggregateRoot, EventAccessor
		{
			return (T) _instance ?? Activator.CreateInstance(typeof (T)) as T;
		}

		public void Save<T>(T aggregate, NewId commitId, IDictionary<string, string> headers) where T : class, AggregateRoot
		{
			SavedAggregate = aggregate;
		}
	}
}
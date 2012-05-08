﻿// Copyright 2012 Henrik Feldt
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
using System.Collections;
using System.Linq;
using Documently.Messages;
using Machine.Specifications;
using Magnum.Extensions;

namespace Documently.Domain.CommandHandlers.Tests.Framework
{
	public static class EventListExtensions
	{
		public static void ShouldContain<T>(this IEnumerable items)
			where T : DomainEvent
		{
			ShouldContain<T>(items, e => { });
		}

		public static void ShouldContain<T>(this IEnumerable items, Action<T> withEvent)
			where T : DomainEvent
		{
			var found = items.Cast<object>().Where(item => item.Implements<T>());

			if (!found.Any())
				throw new SpecificationException(string.Format("Could not find event {0} in list of events: [{1}]",
				                                               typeof (T).Name,
				                                               string.Join("; ", items.Cast<object>()
				                                                                 	.SelectMany(x => x.GetType().GetInterfaces())
																					.Select(t => t.Name))));

			withEvent(found.Cast<T>().First());
		}
	}
}
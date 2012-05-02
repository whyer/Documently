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

using Documently.Messages;
using Magnum.Reflection;

namespace Documently.Domain
{
	public static class AggregateRootEx
	{
		internal static void Raise<TAr, T>(this TAr ar, T evt)
			where TAr : AggregateRoot, EventAccessor
			where T : class, DomainEvent
		{
			ar.Events.RaiseEvent(evt);
		}

		internal static void Raise<TAr, T>(this TAr ar, object anonymousDictionary)
			where TAr : AggregateRoot, EventAccessor
			where T : class, DomainEvent
		{
			var evt = InterfaceImplementationExtensions.InitializeProxy<T>(anonymousDictionary);
			ar.Events.RaiseEvent(evt);
		}
	}
}
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
using System.Dynamic;
using Documently.Messages;
using Magnum.Reflection;
using MassTransit;

namespace Documently.Domain
{
	public static class AggregateRootEx
	{
		internal static void Raise<TAr, T>(this TAr ar, T evt)
			where TAr : AggregateRoot, EventAccessor
			where T : class, DomainEvent
		{
			//evt = SetEmpties(ar, evt);
			ar.Events.RaiseEvent(evt);
		}

		internal static void Raise<TAr, T>(this TAr ar, object anonymousDictionary)
			where TAr : AggregateRoot, EventAccessor
			where T : class, DomainEvent
		{
			var evt = InterfaceImplementationExtensions.InitializeProxy<T>(anonymousDictionary);
			//evt = SetEmpties(ar, evt);
			ar.Events.RaiseEvent(evt);
		}

		//static T SetEmpties<TAr, T>(TAr ar, T evt)
		//    where TAr : AggregateRoot, EventAccessor
		//    where T : class, DomainEvent
		//{
		//    if (evt.Version == 0)
		//        evt = Merge(evt, new
		//            {
		//                Version = ar.Version + 1
		//            });

		//    if (evt.AggregateId == NewId.Empty)
		//        evt = Merge(evt, new
		//            {
		//                AggregateId = ar.Id
		//            });

		//    return evt;
		//}

		//static T Merge<T>(T item1, object item2)
		//    where T : class
		//{
		//    if (item1 == null) throw new ArgumentNullException("item1");
		//    if (item2 == null) throw new ArgumentNullException("item2");

		//    dynamic expando = new ExpandoObject();
		//    var result = expando as IDictionary<string, object>;
			
		//    foreach (System.Reflection.PropertyInfo fi in item1.GetType().GetProperties())
		//        result[fi.Name] = fi.GetValue(item1, null);
			
		//    foreach (System.Reflection.PropertyInfo fi in item2.GetType().GetProperties())
		//        result[fi.Name] = fi.GetValue(item2, null);

		//    return InterfaceImplementationExtensions.InitializeProxy<T>(result);
		//}

	}
}
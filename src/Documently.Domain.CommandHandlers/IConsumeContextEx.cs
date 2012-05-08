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
using System.Collections.Generic;
using System.Linq;
using MassTransit;
using MassTransit.Util;

namespace Documently.Domain.CommandHandlers
{
	public static class IConsumeContextEx
	{
		public static IDictionary<string,string> GetHeaders<T>([NotNull] this IConsumeContext<T> context)
			where T:class
		{
			if (context == null) throw new ArgumentNullException("context");
			return context.Headers.ToDictionary(x => x.Key, x => x.Value);
		}

		public static Guid GetMessageId<T>([NotNull] this IConsumeContext<T> context)
			where T : class
		{
			if (context == null) throw new ArgumentNullException("context");
			return new Guid(context.MessageId);
		}
	}
}
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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MassTransit;
using NUnit.Framework;
using System.Linq;

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedMember.Global

namespace Documently.Domain.CommandHandlers.Tests
{
	public class NextId_spec
	{
		[Test]
		public void nextids_not_equal()
		{
			var id1 = NewId.Next();
			var id2 = NewId.Next();
			Assert.AreNotEqual(id1, id2);
			Assert.AreNotEqual(id1.ToGuid(), id2.ToGuid());
			Assert.IsFalse(id1.ToByteArray().SequenceEqual(id2.ToByteArray()));
		}

		[Test]
		public void is_it_stochastic_perhaps()
		{
			for (int i = 0; i < 10000; i++)
				nextids_not_equal();
		}

		[Test, Description("Related to https://groups.google.com/forum/#!msg/masstransit-discuss/hIBg9eaj1Zk/khk0xwxQ55YJ")]
		public void is_it_serializable()
		{
			NewId id2;
			var id = NewId.Next();
			using (var ms = new MemoryStream())
			{
				new BinaryFormatter().Serialize(ms, id);
				id2 = new NewId(ms.ToArray());
			}
			Assert.That(id2.ToByteArray().SequenceEqual(id.ToByteArray()));
		}

		[Test, Description("Related to https://github.com/joliver/EventStore/issues/112")]
		public void Assignable()
		{
			Assert.That(X<IEnumerable<A>>(), Is.True);
			Assert.That(X<List<A>>(), Is.False);
		}

		static bool X<T>()
		{
			//return typeof(IEnumerable).IsAsysignableFrom(typeof(T));
			return typeof (T).IsInterface && typeof (IEnumerable).IsAssignableFrom(typeof (T));
		}

		class A
		{
		}
	}
}
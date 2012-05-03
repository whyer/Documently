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

using MassTransit;
using NUnit.Framework;
using System.Linq;

namespace Documently.Domain.CommandHandlers.Tests
{
	public class Misc_spec
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
	}
}
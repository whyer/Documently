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

using System.Linq;
using Documently.Domain;
using Documently.Domain.CommandHandlers.ForDocCollection;
using Documently.Messages.DocCollectionCmds;
using Documently.Messages.DocCollectionEvents;
using MassTransit;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_creating_new_documentCollection_no_name_given
		: CommandTestFixture<Create, CreateHandler, DocumentCollection>
	{
		protected override Create When()
		{
			return null;//new Create(NewId.Next(), string.Empty);
		}

		[Test]
		public void should_use_default_name_if_none_given()
		{
			var evt = (Created) PublishedEventsT.First();
			evt.Name.Should().Be("Default name");
		}
	}
}
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
using System.Linq;
using Documently.Commands;
using Documently.Domain;
using Documently.Domain.CommandHandlers;
using Documently.Messages.DocMetaCommands;
using Documently.Messages.DocumentMetaData;
using Magnum;
using NUnit.Framework;
using SharpTestsEx;

namespace Documently.Specs.Documents
{
	public class when_creating_new_document_meta_data
		: CommandTestFixture<Create, DocumentMetaDataHandler, DocumentMetaData>
	{
		private readonly DateTime _created = DateTime.UtcNow;
		private readonly Guid _documentId = CombGuid.Generate();

		protected override Create When()
		{
			return new Create(_documentId, "My document", _created);
		}

		[Test]
		public void should_get_created_document_event()
		{
			var evt = (Created) PublishedEventsT.First();
			evt.Title.Should().Be("My document");
			evt.UtcDate.Should().Be(_created);
			evt.AggregateId.Should().Be(_documentId);
		}
	}
}
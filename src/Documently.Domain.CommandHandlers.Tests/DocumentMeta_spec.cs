// Copyright 2012 Christian Jacobsen
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
using Documently.Domain.CommandHandlers.ForDocMeta;
using Documently.Domain.CommandHandlers.Tests.Behaviors;
using Documently.Domain.CommandHandlers.Tests.Framework;
using Documently.Messages.DocMetaCommands;
using Documently.Messages.DocMetaEvents;
using Machine.Specifications;
using Magnum;

namespace Documently.Domain.CommandHandlers.Tests
{
	[Subject(typeof (DocMeta))]
	public class when_creating_document_meta
		: Handler_and_Aggregate_spec
	{
		static DocumentMetaDataHandler handler;

		Establish context = () =>
			{
				setup_repository_for<DocMeta>();
				handler = new DocumentMetaDataHandler(() => repo);
			};

		Because of = () =>
			handler.Consume(a_command<Create>(new MsgImpl.CreateDocumentMeta
			{
				AggregateId = CombGuid.Generate(),
				Title = "A new document",
				Version = 1U
			}));

		It should_yield_docment_created = () => yieldedEvents.ShouldContain<Created>();
		It should_specify_correct_name = () => 
			yieldedEvents.ShouldContain<Created>(r =>
				r.Title.ShouldEqual("A new document"));

		Behaves_like<Event_versions_are_greater_than_zero> should_specify_versions_above_zero;
		Behaves_like<Event_versions_are_monotonically_increasing> should_specify_monotonically_increasing_versions;
		Behaves_like<Events_has_non_default_aggregate_root_id> should_have_non_default_ar_ids;
	}
}


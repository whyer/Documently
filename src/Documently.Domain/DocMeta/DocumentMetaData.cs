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
using System.Diagnostics.Contracts;
using System.Linq;
using Documently.Messages.DocMetaEvents;
using Documently.Messages.DocumentMetaData;
using MassTransit;
using MassTransit.Util;

namespace Documently.Domain
{
	public class DocMeta : AggregateRoot, EventAccessor
	{
		private DocMeta()
		{
			_state = EventRouter.For(this);
		}

		public DocMeta(NewId documentId, string title, DateTime utcCreated)
			: this()
		{
			var evt = new Created(
				documentId, title, utcCreated);

			this.Raise<DocMeta, Created>(evt);
		}

		public void Apply(Created evt)
		{
			Id = evt.AggregateId;
		}

		public void AssociateWithDocumentBlob(NewId blobId)
		{
			var evt = new DocumentUploaded(blobId, Id, Version + 1);
			this.RaiseEvent(evt);
		}

		public void Apply(DocumentUploaded evt)
		{
			_documentBlobId = evt.BlobId;
		}

		public void AssociateWithCollection(NewId collectionId)
		{
			var evt = new AssociatedWithCollection(Id, collectionId, Version + 1);
			RaiseEvent(evt);
		}

		public void Apply(AssociatedWithCollection evt)
		{
		}

		public void Apply(WasShared evt)
		{
			Id = evt.AggregateId;
			Version = evt.Version;
		}

		private NewId _documentBlobId;
		private EventRouter _state;

		public void ShareWith([NotNull] IEnumerable<int> userIds)
		{
			if (userIds == null) throw new ArgumentNullException("userIds");
			this.Raise<DocMeta, WasShared>(new
				{
					Id,
					Version,
					UserIds = userIds
				});
		}

		public NewId Id { get; private set; }
		public uint Version { get; private set; }
		public EventRouter Events { get; private set; }
	}
}
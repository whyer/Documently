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

namespace Documently.Domain
{
	public class DocumentMetaData : AggregateBase
	{
		public DocumentMetaData()
		{
		}

		public DocumentMetaData(Guid documentId, string title, DateTime utcCreated)
		{
			var @event = new Created(
				documentId, title, utcCreated);

			RaiseEvent(@event);
		}

		public void Apply(Created evt)
		{
			Id = evt.AggregateId;
		}

		public void AssociateWithDocumentBlob(Guid blobId)
		{
			var evt = new DocumentUploaded(blobId, Id, Version + 1);
			RaiseEvent(evt);
		}

		public void Apply(DocumentUploaded evt)
		{
			_documentBlobId = evt.BlobId;
		}

		public void AssociateWithCollection(Guid collectionId)
		{
			var @event = new AssociatedWithCollection(Id, collectionId, Version + 1);
			RaiseEvent(@event);
		}

		public void Apply(AssociatedWithCollection @event)
		{
		}

		public void Apply(WasShared @event)
		{
			Id = @event.AggregateId;
			Version = @event.Version;
		}

		private Guid _documentBlobId;

		public void ShareWith(IEnumerable<int> userIDs)
		{
			Contract.Requires(userIDs != null);
			Contract.Requires(userIDs.Count() > 0);
			Contract.Ensures(Contract.OldValue(userIDs) == userIDs);
			var @event = new WasShared(Id, Version, userIDs);
			RaiseEvent(@event);
		}
	}
}
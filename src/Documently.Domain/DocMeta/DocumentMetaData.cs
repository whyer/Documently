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
using Documently.Messages.DocMetaEvents;
using MassTransit;
using MassTransit.Util;

namespace Documently.Domain
{
	public class DocMeta : AggregateRoot, EventAccessor
	{
		private DocMeta()
		{
			_events = EventRouter.For(this);
		}

		public DocMeta(Guid id, string title, DateTime utcCreated)
			: this()
		{
			this.Raise<DocMeta, Created>(new DocMetaImpl
				{
					CorrelationId = id,
					AggregateId = id,
					Title = title,
					UtcDate = utcCreated,
					Version = Version + 1
				});
		}

		public void Apply(Created evt)
		{
			Id = evt.AggregateId;
		}

		public Guid Id { get; private set; }
		public uint Version { get; private set; }

		private Uri _dataUri;
		private readonly EventRouter _events;

		public EventRouter Events { get { return _events; } }

		public void AssociateWithData(Uri dataUri)
		{
			this.Raise<DocMeta, DocumentUploaded>(new
				{
					AggregateId = Id,
					Version = Version + 1,
					Data = dataUri
				});
		}

		public void Apply(DocumentUploaded evt)
		{
			_dataUri = evt.Data;
		}

		public void AssociateWithCollection(Guid collectionId)
		{
			this.Raise<DocMeta, AssociatedWithCollection>(new
				{
					AggregateId = Id,
					CollectionId = collectionId,
					Version = Version + 1
				});
		}

		public void Apply(AssociatedWithCollection evt)
		{
		}

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

		public void Apply(WasShared evt)
		{
		}
	}

	public class DocMetaImpl : Created
	{
		public Guid AggregateId { get; set; }
		public uint Version { get; set; }
		public string Title { get; set; }
		public DateTime UtcDate { get; set; }
		public Guid CorrelationId { get; set; }
	}
}
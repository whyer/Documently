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
using Caliburn.Micro;
using Documently.Infrastructure;
using Documently.Messages.DocMetaCommands;
using MassTransit;

namespace Documently.WpfClient.Modules.DocumentDetails.CreateMeta
{
	public class CreateDocumentMetaDataViewModel : Screen
	{
		private readonly IBus _bus;
		private readonly IEventAggregator _eventAggregator;

		public CreateDocumentMetaDataViewModel(IBus bus, IEventAggregator eventAggregator)
		{
			_bus = bus;
			_eventAggregator = eventAggregator;
			Command = new SaveDocumentMetaDataModel();
		}

		protected SaveDocumentMetaDataModel Command { get; private set; }

		public void Save()
		{
			_bus.Send<Create>(new CreateImpl
				{
					AggregateId = NewId.Next(),
					Version = 0,
					Title = Command.Title,
					UtcTime = DateTime.UtcNow
				});
			_eventAggregator.Publish(new DocumentMetaDataSaved());
		}
	}

	internal class CreateImpl : Create
	{
		public NewId AggregateId { get; set; }
		public uint Version { get; set; }
		public string Title { get; set; }
		public DateTime UtcTime { get; set; }
	}

	public class SaveDocumentMetaDataModel
	{
		public string Title { get; set; }
	}

	public class DocumentMetaDataSaved
	{
	}
}
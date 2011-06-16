using System;
using Documently.Domain.Domain;

namespace Documently.Domain.Events
{
	public class DocumentMetaDataCreated : DomainEvent
	{
		private readonly string _Title;
		private readonly DocumentState _ProcessingState;
		private readonly DateTime _UtcCreated;

		public DocumentMetaDataCreated(Guid documentId, string title, DocumentState state, DateTime utcCreated)
		{
			_Title = title;
			_ProcessingState = state;
			_UtcCreated = utcCreated;
			AggregateId = documentId;
		}

		public string Title
		{
			get { return _Title; }
		}

		public DocumentState ProcessingState
		{
			get {
				return _ProcessingState;
			}
		}

		public DateTime UtcDate
		{
			get {
				return _UtcCreated;
			}
		}
	}
}
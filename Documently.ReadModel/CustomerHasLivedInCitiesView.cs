using Documently.Domain.Events;
using Raven.Client;

namespace CQRSSample.ReadModel
{
	class CustomerHasLivedInCitiesView : HandlesEvent<CustomerCreatedEvent>, HandlesEvent<CustomerRelocatedEvent>
	{
		private readonly IDocumentStore _documentStore;

		public CustomerHasLivedInCitiesView(IDocumentStore documentStore)	
		{
			_documentStore = documentStore;
		}

		public void Handle(CustomerCreatedEvent @event)
		{
			using (var session = _documentStore.OpenSession())
			{
				var dto = session.Load<CustomerHasLivedInDto>(Dto.GetDtoIdOf<CustomerHasLivedInDto>(@event.AggregateId));
				dto.AddCity(@event.City);
				session.SaveChanges();
			}
		}

		public void Handle(CustomerRelocatedEvent @event)
		{
			using (var session = _documentStore.OpenSession())
			{
				var dto = new CustomerHasLivedInDto()
				          	{
				          		AggregateRootId = @event.AggregateId
				          	};

				dto.AddCity(@event.City);

				session.Store(dto);
				session.SaveChanges();
			}
		}
	}
}
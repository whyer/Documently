using Documently.Messages;
using Documently.Messages.CustomerEvents;
using Raven.Client;

namespace Documently.ReadModel
{
	class CustomerHasLivedInCitiesView : HandlesEvent<Created>, HandlesEvent<Relocated>
	{
		private readonly IDocumentStore _documentStore;

		public CustomerHasLivedInCitiesView(IDocumentStore documentStore)	
		{
			_documentStore = documentStore;
		}

		public void Consume(Created @event)
		{
			using (var session = _documentStore.OpenSession())
			{
				var dto = session.Load<CustomerHasLivedInDto>(Dto.GetDtoIdOf<CustomerHasLivedInDto>(@event.AggregateId));
				dto.AddCity(@event.City);
				session.SaveChanges();
			}
		}

		public void Consume(Relocated @event)
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
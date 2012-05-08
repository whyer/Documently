using Documently.Messages.CustEvents;
using Raven.Client;

namespace Documently.ReadModel
{
	class CustomerHasLivedInCitiesView : HandlesEvent<Registered>, HandlesEvent<Relocated>
	{
		private readonly IDocumentStore _documentStore;

		public CustomerHasLivedInCitiesView(IDocumentStore documentStore)	
		{
			_documentStore = documentStore;
		}

		public void Consume(Registered evt)
		{
			using (var session = _documentStore.OpenSession())
			{
				var dto = session.Load<CustomerHasLivedInDto>(Dto.GetDtoIdOf<CustomerHasLivedInDto>(evt.AggregateId));
				dto.AddCity(evt.Address.City);
				session.SaveChanges();
			}
		}

		public void Consume(Relocated evt)
		{
			using (var session = _documentStore.OpenSession())
			{
				var dto = new CustomerHasLivedInDto
					{
						AggregateId = evt.AggregateId
					};

				dto.AddCity(evt.City);

				session.Store(dto);
				session.SaveChanges();
			}
		}
	}
}
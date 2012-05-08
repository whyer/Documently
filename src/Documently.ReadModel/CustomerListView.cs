using Documently.Messages;
using Documently.Messages.CustEvents;
using Raven.Client;

namespace Documently.ReadModel
{
	public class CustomerListView : HandlesEvent<Registered>, HandlesEvent<Relocated>
	{
		private readonly IDocumentStore _DocumentStore;

		public CustomerListView(IDocumentStore documentStore)
		{
			_DocumentStore = documentStore;
		}

		public void Consume(Relocated evt)
		{
			using (var session = _DocumentStore.OpenSession())
			{
				var dto = session.Load<CustomerListDto>(Dto.GetDtoIdOf<CustomerListDto>(evt.AggregateId));
				dto.City = evt.City;
				session.SaveChanges();
			}
		}

		public void Consume(Registered evt)
		{
			using (var session = _DocumentStore.OpenSession())
			{
				var dto = new CustomerListDto {AggregateId = evt.AggregateId, City = evt.Address.City, Name = evt.CustomerName};
				session.Store(dto);
				session.SaveChanges();
			}
		}
	}
}
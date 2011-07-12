using Documently.Domain.Events;
using Raven.Client;

namespace Documently.ReadModel
{
	public class CustomerListView : HandlesEvent<CustomerCreatedEvent>, HandlesEvent<CustomerRelocatedEvent>
	{
		private readonly IDocumentStore _DocumentStore;

		public CustomerListView(IDocumentStore documentStore)
		{
			_DocumentStore = documentStore;
		}

		public void Consume(CustomerRelocatedEvent @event)
		{
			using (var session = _DocumentStore.OpenSession())
			{
				var dto = session.Load<CustomerListDto>(Dto.GetDtoIdOf<CustomerListDto>(@event.AggregateId));
				dto.City = @event.City;
				session.SaveChanges();
			}
		}

		public void Consume(CustomerCreatedEvent @event)
		{
			using (var session = _DocumentStore.OpenSession())
			{
				var dto = new CustomerListDto {AggregateRootId = @event.AggregateId, City = @event.City, Name = @event.CustomerName};
				session.Store(dto);
				session.SaveChanges();
			}
		}
	}
}
using Documently.Messages;
using Documently.Messages.CustEvents;
using Raven.Client;

namespace Documently.ReadModel
{
	public class CustomerAddressView : HandlesEvent<Created>, HandlesEvent<Relocated>
	{
		private readonly IDocumentStore _documentStore;

		public CustomerAddressView(IDocumentStore documentStore)
		{
			_documentStore = documentStore;
		}

		public void Consume(Relocated evt)
		{
			using (var session = _documentStore.OpenSession())
			{
				var dto = session.Load<CustomerAddressDto>(Dto.GetDtoIdOf<CustomerAddressDto>(evt.AggregateId));
				dto.Street = evt.Street;
				dto.StreetNumber = evt.StreetNumber;
				dto.PostalCode = evt.PostalCode;
				dto.City = evt.City;
				session.SaveChanges();
			}
		}

		public void Consume(Created evt)
		{
			using (var session = _documentStore.OpenSession())
			{
				var dto = new CustomerAddressDto
				{
					AggregateId = evt.AggregateId,
					CustomerName = evt.CustomerName,
					Street = evt.Street,
					StreetNumber = evt.StreetNumber,
					PostalCode = evt.PostalCode,
					City = evt.City
				};
				session.Store(dto);
				session.SaveChanges();
			}
		}
	}
}
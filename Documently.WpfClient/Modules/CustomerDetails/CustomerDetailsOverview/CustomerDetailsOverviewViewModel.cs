using System;
using CQRSSample.ReadModel;
using Caliburn.Micro;

namespace Documently.WpfClient.Modules.CustomerDetails.CustomerDetailsOverview
{
	public class CustomerDetailsOverviewViewModel : Screen, IShowCustomerDetails
	{
		private readonly IReadRepository _readRepository;

		public CustomerDetailsOverviewViewModel(IReadRepository readRepository)
		{
			_readRepository = readRepository;
		}

		public void WithCustomer(string customerDtoId)
		{
			ViewModel = _readRepository.GetById<CustomerListDto>(customerDtoId);
		}

		public Guid GetCustomerId()
		{
			return ViewModel.AggregateRootId;
		}

		//TODO: Change CustomerListDto to something specific for this screen
		public CustomerListDto ViewModel { get; private set; }
	}
}
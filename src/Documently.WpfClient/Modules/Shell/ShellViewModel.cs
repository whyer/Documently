using System;
using Caliburn.Micro;
using Documently.Infrastructure;
using Documently.ReadModel;
using Documently.WpfClient.Modules.CustomerDetails;
using Documently.WpfClient.Modules.CustomerDetails.CreateCustomer;
using Documently.WpfClient.Modules.CustomerDetails.CustomerDetailsOverview;
using Documently.WpfClient.Modules.CustomerDetails.CustomerRelocating;
using Documently.WpfClient.Modules.CustomerDetails.WhatsNext;
using Documently.WpfClient.Modules.CustomerList;
using Documently.WpfClient.Modules.DocumentDetails.CreateMeta;
using Documently.WpfClient.Modules.DocumentSearch;

namespace Documently.WpfClient.Modules.Shell
{
	public class ShellViewModel : Conductor<object>,
	                              IHandle<CreateCustomerSavedEvent>,
	                              IHandle<ShowAddNewCustomerEvent>,
								  IHandle<ShowSearchCustomerEvent>,
	                              IHandle<CustomerRelocatingSavedEvent>,
	                              IHandle<ShowCustomerDetailsEvent>,
		IHandle<DocumentMetaDataSaved>
	{
		private readonly IReadRepository _repository;
		private readonly IBus _bus;
		private readonly IEventAggregator _eventAggregator;
		private Guid? _aggregateRootId;

		public ShellViewModel(IReadRepository repository, IBus bus, IEventAggregator eventAggregator)
		{
			_repository = repository;
			_bus = bus;
			_eventAggregator = eventAggregator;
			_eventAggregator.Subscribe(this);

			SearchCustomer();
		}

		public override void ActivateItem(object item)
		{
			base.ActivateItem(item);

			_aggregateRootId = null;
			var screen = item as IShowCustomerDetails;
			ShowCustomerDetailButtons = screen != null;
			if (screen != null)
				_aggregateRootId = screen.GetCustomerId();
		}

		// customers
		private bool _showCustomerDetailButtons;

		public bool ShowCustomerDetailButtons
		{
			get { return _showCustomerDetailButtons; }
			private set
			{
				_showCustomerDetailButtons = value;
				NotifyOfPropertyChange(() => ShowCustomerDetailButtons);
			}
		}

		public void AddNewCustomer()
		{
			ActivateItem(new CreateCustomerViewModel(_bus, _eventAggregator));
		}

		public void SearchCustomer()
		{
			ActivateItem(new CustomerListViewModel(_repository, _eventAggregator));
		}

		public void RelocateCustomer()
		{
			var screen = new CustomerRelocatingViewModel(_bus, _eventAggregator, _repository);
			screen.WithCustomer(_aggregateRootId.Value);
			ActivateItem(screen);
		}


		// docs
		private void SearchDocument()
		{
			ActivateItem(new DocumentSearchViewModel(_bus, _eventAggregator));
		}

		//Handles 
		public void Handle(CreateCustomerSavedEvent message)
		{
			ActivateItem(new WhatsNextViewModel(_eventAggregator));
		}

		public void Handle(ShowAddNewCustomerEvent message)
		{
			AddNewCustomer();
		}

		public void Handle(ShowSearchCustomerEvent message)
		{
			SearchCustomer();
		}

		public void Handle(CustomerRelocatingSavedEvent message)
		{
			ActivateItem(new WhatsNextViewModel(_eventAggregator));
		}

		public void Handle(ShowCustomerDetailsEvent message)
		{
			var screen = new CustomerDetailsOverviewViewModel(_repository);
			screen.WithCustomer(message.DtoId);
			ActivateItem(screen);
		}

		public void Handle(DocumentMetaDataSaved message)
		{
			SearchDocument();
		}
	}
}
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
using MassTransit;

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
		private readonly IReadRepository _Repository;
		private readonly IBus _Bus;
		private readonly IEventAggregator _EventAggregator;
		private Guid? _AggregateRootId;

		public ShellViewModel(IReadRepository repository, IBus bus, IEventAggregator eventAggregator)
		{
			_Repository = repository;
			_Bus = bus;
			_EventAggregator = eventAggregator;
			_EventAggregator.Subscribe(this);

			SearchCustomer();
		}

		public override void ActivateItem(object item)
		{
			base.ActivateItem(item);

			_AggregateRootId = null;
			var screen = item as IShowCustomerDetails;
			ShowCustomerDetailButtons = screen != null;
			if (screen != null)
				_AggregateRootId = screen.GetCustomerId();
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
			ActivateItem(new CreateCustomerViewModel(_Bus, _EventAggregator));
		}

		public void SearchCustomer()
		{
			ActivateItem(new CustomerListViewModel(_Repository, _EventAggregator));
		}

		public void RelocateCustomer()
		{
			var screen = new CustomerRelocatingViewModel(_Bus, _EventAggregator, _Repository);
			screen.WithCustomer(_AggregateRootId.Value);
			ActivateItem(screen);
		}


		// docs
		private void SearchDocument()
		{
			ActivateItem(new DocumentSearchViewModel(_Bus, _EventAggregator));
		}

		//Handles 
		public void Handle(CreateCustomerSavedEvent message)
		{
			ActivateItem(new WhatsNextViewModel(_EventAggregator));
		}

		public void Handle(ShowAddNewCustomerEvent message)
		{
			AddNewCustomer();
		}

		public void Handle(ShowSearchCustomerEvent message)
		{
			SearchCustomer();
		}

		public void CreateDocumentMetaData() {
			ActivateItem(new CreateDocumentMetaDataViewModel(_Bus, _EventAggregator));
		}

		public void Handle(CustomerRelocatingSavedEvent message)
		{
			ActivateItem(new WhatsNextViewModel(_EventAggregator));
		}

		public void Handle(ShowCustomerDetailsEvent message)
		{
			var screen = new CustomerDetailsOverviewViewModel(_Repository);
			screen.WithCustomer(message.DtoId);
			ActivateItem(screen);
		}

		public void Handle(DocumentMetaDataSaved message)
		{
			SearchDocument();
		}
	}
}
using System;
using System.Net;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Commands;
using Documently.Infrastructure;
using Documently.Infrastructure.Installers;
using Documently.Messages.CustCommands;
using Documently.Messages.DocCollectionCmds;
using Documently.Messages.DocMetaCommands;
using Documently.ReadModel;
using Magnum;
using MassTransit;
using NLog;
using NLog.Config;
using Raven.Client;
using Create = Documently.Messages.DocCollectionCmds.Create;

namespace Documently.App
{
	internal class Program
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		
		private IWindsorContainer _container;
		private IEndpoint _domainService;

		private static void Main()
		{
			Description();
			SimpleConfigurator.ConfigureForConsoleLogging();

			var p = new Program();
			try { p.Start(); }
			finally { p.Stop(); }
		}

		private void Start()
		{
			try
			{
				_logger.Info("installing and setting up components");

				_container = new WindsorContainer()
					.Install(
						new RavenDbServerInstaller(),
						new ReadRepositoryInstaller(),
						new BusInstaller("rabbitmq://localhost/Documently.App"),
						new EventStoreInstaller());

				_container.Register(Component.For<IWindsorContainer>().Instance(_container));

				var bus = _container.Resolve<IServiceBus>();
				_domainService = bus.GetEndpoint(new Uri(Keys.DomainServiceEndpoint));

				var customerId = CombGuid.Generate();

				Console.WriteLine("create new customer by pressing a key");
				Console.ReadKey(true);

				//create customer (Write/Command)
				RegisterNewCustomer(customerId);

				Console.WriteLine("Customer created. Press any key to relocate customer.");
				Console.ReadKey(true);

				//Customer relocating (Write/Command)
				RelocateCustomer(customerId, 1);

				Console.WriteLine("Customer relocated. Press any key to show list of customers.");
				Console.ReadKey(true);

				//show all customers [in RMQ] (Read/Query)
				ShowCustomerListView();
			}
			catch (WebException ex)
			{
				_logger.Error(@"Unable to connect to RavenDB Server. Have you started 'RavenDB\Server\Raven.Server.exe'?", ex);
			}
			catch (Exception ex)
			{
				_logger.Error("exception thrown", ex);
			}

			Console.WriteLine("Press any key to finish.");
			Console.ReadKey(true);
		}

		private static void Description()
		{
			Console.WriteLine(@"This application:
* Creates a customer
* Relocates the customer
* Shows all customers.");
		}

		private void ShowCustomerListView()
		{
			var store = _container.Resolve<IDocumentStore>();

			using (var session = store.OpenSession())
			{
				foreach (var dto in session.Query<CustomerListDto>())
				{
					Console.WriteLine(dto.Name + " now living in " + dto.City + " (" + dto.AggregateRootId + ")");
					Console.WriteLine("---");
				}
			}
		}

		private void RegisterNewCustomer(Guid aggregateId)
		{
			_domainService.Send(new CreateCustImpl(aggregateId, "Jörg Egretzberger", 
				"Meine Straße", "1", "1010", "Wien", "01/123456"));
		}

		private void RelocateCustomer(Guid customerId, uint prevVersion)
		{
			_domainService.Send(new RelocateImpl(customerId, prevVersion,
				"Messestraße", "2", "4444", "Linz"));
		}

		private void Stop()
		{
			_container.Dispose();
		}
	}

	class RelocateImpl : RelocateTheCustomer
	{
		public RelocateImpl(Guid aggregateId, uint version, string street, string streetnumber, string postalCode, string city)
		{
			AggregateId = aggregateId;
			Version = version;
			Street = street;
			Streetnumber = streetnumber;
			PostalCode = postalCode;
			City = city;
		}

		public Guid AggregateId { get; set; }
		public uint Version { get; set; }

		public string Street { get; set; }
		public string Streetnumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
	}

	class CreateCustImpl : RegisterNew
	{
		public CreateCustImpl(Guid aggregateId, string customerName, string street, string streetNumber,
			string postalCode, string city, string phoneNumber)
		{
			AggregateId = aggregateId;
			CustomerName = customerName;
			Street = street;
			StreetNumber = streetNumber;
			PostalCode = postalCode;
			City = city;
			PhoneNumber = phoneNumber;
			Version = 0;
		}

		public Guid AggregateId { get; set; }
		public uint Version { get; set; }
		public string CustomerName { get; set; }
		public string Street { get; set; }
		public string StreetNumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
		public string PhoneNumber { get; set; }
	}
}
using System;
using System.Net;
using CQRSSample.ReadModel;
using Castle.Windsor;
using Documently.Commands;
using Documently.Infrastructure;
using Magnum;
using Raven.Client;
using Raven.Client.Document;

namespace CQRSSample.App
{
	internal class Program
	{
		private static void Main()
		{
			Description();

			//use RavenDB Server as an event store and persists the read side (views) also to RavenDB Server
			var viewStore = new DocumentStore {ConnectionStringName = BootStrapper.RavenDbConnectionStringName};
			viewStore.Initialize();

			//run RavenDB InMemory
			//var store = new EmbeddableDocumentStore {RunInMemory = true};

			IWindsorContainer container = null;
			try
			{
				container = BootStrapper.BootStrap(viewStore);

				var bus = container.Resolve<IBus>();
				var aggregateId = CombGuid.Generate();

				//create customer (Write/Command)
				CreateCustomer(bus, aggregateId);

				//Customer relocating (Write/Command)
				RelocateCustomer(bus, aggregateId);

				//show all customers [in RMQ] (Read/Query)
				ShowCustomerListView(viewStore);
			}
			catch (WebException ex)
			{
				Console.WriteLine(@"Unable to connect to RavenDB Server. Have you started 'RavenDB\Server\Raven.Server.exe'?");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Fehler: " + ex.Message);
			}
			finally
			{
				if (container != null) container.Dispose();
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

		private static void ShowCustomerListView(IDocumentStore store)
		{
			using (var session = store.OpenSession())
			{
				foreach (var dto in session.Query<CustomerListDto>())
				{
					Console.WriteLine(dto.Name + " now living in " + dto.City + " (" + dto.AggregateRootId + ")");
					Console.WriteLine("---");
				}
			}
		}

		private static void RelocateCustomer(IBus bus, Guid aggregateId)
		{
			bus.Send(new RelocateCustomerCommand(aggregateId, "Messestraße", "2", "4444", "Linz"));

			Console.WriteLine("Customer relocated. Press any key to show list of customers.");
			Console.ReadLine();
		}

		private static void CreateCustomer(IBus bus, Guid aggregateId)
		{
			bus.Send(new CreateCustomerCommand(aggregateId, "Jörg Egretzberger", "Meine Straße", "1", "1010", "Wien", "01/123456"));
			Console.WriteLine("Customer created. Press any key to relocate customer.");
			Console.ReadLine();
		}
	}
}
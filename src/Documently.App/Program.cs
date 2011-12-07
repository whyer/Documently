using System;
using System.Net;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Documently.Commands;
using Documently.Domain.Events;
using Documently.Infrastructure;
using Documently.Infrastructure.Installers;
using Documently.ReadModel;
using Magnum;
using MassTransit;
using Raven.Client;
using log4net;
using log4net.Config;

namespace Documently.App
{
	internal class Program
	{
		private static readonly ILog _Logger = LogManager.GetLogger(typeof (Program));

		private IWindsorContainer _Container;

		private static void Main()
		{
			Description();
			XmlConfigurator.Configure();

			var p = new Program();
			try
			{
				p.Start();
			}
			finally
			{
				p.Stop();
			}
		}

		private void Start()
		{
			_Logger.Info("installing and setting up components");
			_Container = new WindsorContainer()
				.Install(
					new RavenDbServerInstaller(),
					new ReadRepositoryInstaller(),
					new BusInstaller("rabbitmq://localhost/Documently.App"),
					new EventStoreInstaller());

			_Container.Register(Component.For<IWindsorContainer>().Instance(_Container));

			var bus = _Container.Resolve<IServiceBus>();
			try
			{
				var domainService = bus.GetEndpoint(new Uri(Keys.DomainServiceEndpoint));

				var documentId = CombGuid.Generate();

				Console.WriteLine("Create new Document metadata by pressing a key");
				Console.ReadKey(true);

				//create customer (Write/Command)
				var created = Request<CreateDocumentMetaData, DocumentMetaDataCreated>(
					domainService, bus, new CreateDocumentMetaData(documentId, "DocumentTitle", DateTime.UtcNow));
				
				Console.WriteLine("Document created. Press any key to create document collection");
				Console.ReadKey(true);
				
				var collectionId = CombGuid.Generate();
				// Create collection
				Request<CreateNewDocumentCollection, DocumentCollectionCreated>(
					domainService, bus, new CreateNewDocumentCollection(collectionId, "My new collection", created.Version));
					
				Console.WriteLine("Collection created. Press any key to associate document with collection");
				Console.ReadKey(true);

				// Associate
				var associated = Request<AssociateDocumentWithCollection, AssociatedWithCollection>(
					domainService, bus, new AssociateDocumentWithCollection(documentId, collectionId));

				Console.WriteLine(string.Format("Assocoation done, ar at version#{0}. Press any key to show list of documents.", associated.Version));
				Console.ReadKey(true);

				////show all customers [in RMQ] (Read/Query)
				ShowDocumentList();


			}
			catch (WebException ex)
			{
				_Logger.Error(@"Unable to connect to RavenDB Server. Have you started 'RavenDB\Server\Raven.Server.exe'?", ex);
			}
			catch (Exception ex)
			{
				_Logger.Error("exception thrown", ex);
			}
			finally
			{
				_Container.Release(bus);
			}

			Console.WriteLine("Press any key to finish.");
			Console.ReadKey();
		}

		private TEvent Request<T, TEvent>(IEndpoint domainService, IServiceBus bus, T msg)
			where TEvent : class
			where T : class
		{
			TEvent myEvent = null;
			var eventGotten = new ManualResetEvent(false);
			domainService.SendRequest(msg, bus, req => req.Handle<TEvent>(evt =>
				{
					eventGotten.Set();
					myEvent = evt;
				}));

			eventGotten.WaitOne();
			return myEvent;
		}

		private void ShowDocumentList()
		{
			var store = _Container.Resolve<IDocumentStore>();

			using (var session = store.OpenSession())
			{
				foreach (var documentDto in session.Query<DocumentDto>())
				{
					Console.WriteLine(documentDto.Title + " created at " + documentDto.CreatedUtc + " (" + documentDto.AggregateRootId +
					                  ")");
					Console.WriteLine("---");
				}
			}
		}

		private static void Description()
		{
			Console.WriteLine(
				@"This application:
* Creates a document
* Creates a document collection
* Shows all documents and collections.");
		}

		private void ShowCustomerListView()
		{
			var store = _Container.Resolve<IDocumentStore>();

			using (var session = store.OpenSession())
			{
				foreach (var dto in session.Query<CustomerListDto>())
				{
					Console.WriteLine(dto.Name + " now living in " + dto.City + " (" + dto.AggregateRootId + ")");
					Console.WriteLine("---");
				}
			}
		}

		private void Stop()
		{
			_Container.Dispose();
		}
	}
}
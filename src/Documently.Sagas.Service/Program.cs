using System;
using System.Threading;
using Automatonymous;
using MassTransit;
using MassTransit.NHibernateIntegration.Saga;
using MassTransit.NLogIntegration;
using MassTransit.NLogIntegration.Logging;
using MassTransit.Saga;
using MassTransit.Services.Timeout;
using MassTransit.Services.Timeout.Server;
using NHibernate;
using NLog;
using Topshelf;
using log4net.Config;

namespace Documently.Sagas.Service
{
	public class Program
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private IServiceBus _bus;
		TimeoutService _service;

		public static void Main(string[] args)
		{
			Thread.CurrentThread.Name = "Saga Service Main Thread";
			HostFactory.Run(x =>
			{
				x.Service<Program>(s =>
				{
					s.ConstructUsing(name => new Program());
					s.WhenStarted(p => p.Start());
					s.WhenStopped(p => p.Stop());
				});
				x.RunAsLocalSystem();

				x.SetDescription("Handles the sagas for the Documently Application.");
				x.SetDisplayName("Documently Sagas Service");
				x.SetServiceName("Documently.Sagas.Service");
			});
		}

		private void Start()
		{
			try
			{
				MassTransit.Logging.Logger.UseLogger(new NLogLogger());

				BasicConfigurator.Configure(); // for TopShelf until it upgrades

				_logger.Info("setting up saga service");

				ISessionFactory sessionFactory = NHibernateSagaHelper.CreateSessionFactory();

				_bus = ServiceBusFactory.New(sbc =>
					{
						sbc.UseNLog();
						sbc.UseRabbitMqRouting();
						sbc.ReceiveFrom("rabbitmq://localhost/Documently.Sagas.Service");
						sbc.Subscribe(s =>
						{
							s.StateMachineSaga(new IndexerOrchestrationSaga(),
											   new NHibernateSagaRepository<IndexerOrchestrationSagaInstance>(sessionFactory));
						});
					});

				// The timeout service should probably run in its own Topshelf process when we are done testing the sagas
				_service = new TimeoutService(_bus, new InMemorySagaRepository<TimeoutSaga>());
				//_service.Start();

				_logger.Info("application configured, it is now running");
			}
			catch (Exception ex)
			{
				_logger.FatalException("Error when starting service", ex);
			}
		}

		private void Stop()
		{
			_logger.Info("shutting down Domain Service");
			
			if(_service != null)
				_service.Stop();
			
			if(_bus != null)
				_bus.Dispose();
		}
	}
}

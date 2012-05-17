using System;
using System.Threading;
using Automatonymous;
using Documently.Messages;
using MassTransit;
using MassTransit.Saga;
using MassTransit.NLogIntegration;
using NLog;
using Topshelf;
using log4net.Config;

namespace Documently.Sagas.Service
{
	public class Program
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private IServiceBus _bus;

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
			BasicConfigurator.Configure(); // for TopShelf until it upgrades
			NLog.Config.SimpleConfigurator.ConfigureForConsoleLogging();

			_logger.Info("setting up saga service");

			try
			{
				_bus = ServiceBusFactory.New(sbc =>
				{
					sbc.UseNLog();
					sbc.UseRabbitMqRouting();
					sbc.ReceiveFrom("rabbitmq://localhost/Documently.Sagas.Service");
					sbc.Subscribe(s =>
					{
						s.StateMachineSaga(new IndexerOrchestrationSaga(), new InMemorySagaRepository<Instance>());
					});
				});
			}

			catch(Exception ex)
			{
				_logger.DebugException("Error on bus config", ex);
			}

			_logger.Info("application configured, started running");
		}

		private void Stop()
		{
			_logger.Info("shutting down Domain Service");
			_bus.Dispose();
		}
	}
}

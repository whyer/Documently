using Documently.Sagas.Service.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Documently.Sagas.Service
{
	public class NHibernateSagaHelper
	{
		public static ISessionFactory CreateSessionFactory()
		{
			return Fluently.Configure()
				.Database(
					MsSqlCeConfiguration.Standard.ConnectionString("Data Source=SagaDb.sdf")
				//.ShowSql().FormatSql()
				)
				.Mappings(m => m.FluentMappings.Add<IndexerOrchestrationSagaInstanceMapping>())
				.ExposeConfiguration(configuration => UpdateSchema(configuration))
				.BuildSessionFactory();
		}

		private static void UpdateSchema(Configuration config)
		{
			//// delete the existing db on each run
			//if (File.Exists(DbFile))
			//    File.Delete(DbFile);

			new SchemaUpdate(config).Execute(false, true);
		}

		private static void BuildSchema(Configuration config)
		{
			//// delete the existing db on each run
			//if (File.Exists(DbFile))
			//    File.Delete(DbFile);

			new SchemaExport(config).Drop(true, true);
			new SchemaExport(config).Create(true, true);
		}

	}
}
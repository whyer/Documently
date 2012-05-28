using FluentNHibernate.Mapping;
using MassTransit.NHibernateIntegration;

namespace Documently.Sagas.Service.Mapping
{
	public class IndexerOrchestrationSagaInstanceMapping : ClassMap<IndexerOrchestrationSagaInstance>
	{
		public IndexerOrchestrationSagaInstanceMapping()
		{
			Not.LazyLoad();
			
			Id(x => x.CorrelationId)
				.GeneratedBy.Assigned();

			Map(x => x.CurrentState)
				.CustomType<AutomatonymousSagaStateUserType<IndexerOrchestrationSaga>>();
		}
	}
}
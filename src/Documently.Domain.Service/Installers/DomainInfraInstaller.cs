// Copyright 2012 Henrik Feldt
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Documently.Domain.CommandHandlers.Infrastructure;
using Magnum.Policies;

namespace Documently.Domain.Service.Installers
{
	public class DomainInfraInstaller
		: IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				C<DomainRepository, EventStoreRepository>().DependsOn((a,b) => a.Resolve<ExceptionPolicy>("eventstore")),
				C<AggregateRootFactory, AggregateFactory>());//,
				//C<IDetectConflicts, ConflictDetector>());
		}

		private static ComponentRegistration<TS> C<TS, TC>()
			where TC : TS
			where TS : class
		{
			return Component.For<TS>().ImplementedBy<TC>().LifeStyle.Transient;
		}
	}
}
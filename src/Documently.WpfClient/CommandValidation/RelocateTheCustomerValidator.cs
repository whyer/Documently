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

using Documently.Commands;
using FluentValidation;

namespace Documently.WpfClient.CommandValidation
{
	/// <summary>
	/// This validator validates that the command is correct from an application-validation perspective.
	/// </summary>
	public class RelocateTheCustomerValidator : AbstractValidator<RelocateTheCustomer>
	{
		public RelocateTheCustomerValidator()
		{
			RuleFor(command => command.City)
				.NotEmpty().NotNull();

			RuleFor(c => c.Street)
				.NotEmpty().NotNull();
		}
	}
}
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

using Caliburn.Micro;
using Documently.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace Documently.WpfClient.ApplicationFramework
{
	public class ScreenWithValidatingCommand<T> : Screen where T : Command
	{
		public ValidatingCommand<T> Command { get; protected set; }

		public IValidator<T> Validator { get; set; }

		/// <summary>
		/// 	Validates the command
		/// </summary>
		protected virtual ValidationResult Validate()
		{
			return Command.Validate();
		}
	}
}
using Caliburn.Micro;
using Documently.Commands;
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
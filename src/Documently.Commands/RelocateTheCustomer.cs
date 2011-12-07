using System;
using FluentValidation;

namespace Documently.Commands
{
	[Serializable]
	public class RelocateTheCustomer : Command
	{
		public string Street { get; set; }
		public string Streetnumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }

		/// <summary> for serialization </summary>
		[Obsolete("for serialization")]
		protected RelocateTheCustomer()
		{
		}

		public RelocateTheCustomer(Guid id, string street, string streetNumber, string postalCode, string city)
			: base(id)
		{
			Street = street;
			Streetnumber = streetNumber;
			PostalCode = postalCode;
			City = city;
		}
	}

	/// <summary>
	/// This validator validates that the command is correct from an application-validation perspective.
	/// </summary>
	public class RelocatingCustomerValidator : AbstractValidator<RelocateTheCustomer>
	{
		public RelocatingCustomerValidator()
		{
			RuleFor(command => command.City).NotEmpty().NotNull();
		}
	}
}
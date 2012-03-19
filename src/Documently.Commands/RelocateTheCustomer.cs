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

		public RelocateTheCustomer(Guid arId, string street, string streetNumber, string postalCode, string city)
		{
			AggregateId = arId;
			Street = street;
			Streetnumber = streetNumber;
			PostalCode = postalCode;
			City = city;
		}

		public Guid AggregateId { get; set; }
		public int Version { get; set; }
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
using System;
using FluentValidation;

namespace Documently.Commands
{
	[Serializable]
	public class RelocateCustomerCommand : Command
	{
		public string Street { get; set; }
		public string Streetnumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }

		public RelocateCustomerCommand(Guid id) : base(id)
		{
		}

		public RelocateCustomerCommand(Guid id, string street, string streetNumber, string postalCode, string city)
			: base(id)
		{
			Street = street;
			Streetnumber = streetNumber;
			PostalCode = postalCode;
			City = city;
		}
	}

	public class RelocatingCustomerValidator : AbstractValidator<RelocateCustomerCommand>
	{
		public RelocatingCustomerValidator()
		{
			RuleFor(command => command.City).NotEmpty().NotNull();
		}
	}
}
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

		public RelocateTheCustomer()
		{
		}

		public RelocateTheCustomer(Guid id) : base(id)
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

	public class RelocatingCustomerValidator : AbstractValidator<RelocateTheCustomer>
	{
		public RelocatingCustomerValidator()
		{
			RuleFor(command => command.City).NotEmpty().NotNull();
		}
	}
}
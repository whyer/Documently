using System;
using FluentValidation;

namespace Documently.Commands
{
	public interface RelocateTheCustomer : Command
	{
		string Street { get; set; }
		string Streetnumber { get; set; }
		string PostalCode { get; set; }
		string City { get; set; }
	}
}
using System;

namespace CQRSSample.Domain.Domain
{
	public class NonExistingCustomerException : Exception
	{
		public NonExistingCustomerException(string message) : base(message)
		{
		}
	}
}
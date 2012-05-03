using System;
using Documently.Messages.CustEvents;
using MassTransit;

namespace Documently.Domain.CommandHandlers.Tests
{
	static class CustomerTestFactory
	{
		public static Registered Registered(Guid arId)
		{
			return new RegisteredImpl
				{
					AggregateId = arId,
					CustomerName = "Henrik F",
					PhoneNumber = "+46727344868",
					Address = new MsgImpl.Address
						{
							Street = "Drottninggatan",
							StreetNumber = 108,
							PostalCode = "113 60",
							City = "Stockholm"
						},
					Version = 1U
				};
		} 
	}
}
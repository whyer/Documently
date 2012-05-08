namespace Documently.Domain.CommandHandlers.Tests.MsgImpl
{
	class Address 
		: Messages.CustDtos.Address
	{
		public string Street { get; set; }
		public uint StreetNumber { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
	}
}
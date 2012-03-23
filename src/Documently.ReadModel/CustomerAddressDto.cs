namespace Documently.ReadModel
{
	public class CustomerAddressDto : Dto
	{
		public string City { get; set; }

		public string PostalCode { get; set; }

		public string StreetNumber { get; set; }

		public string Street { get; set; }

		public string CustomerName { get; set; }

		public uint LatestVersionSeen { get; set; }
	}
}
namespace Documently.Domain
{
	public class PhoneNumber
	{
		public PhoneNumber(string phoneNumber)
		{
			Number = phoneNumber;
		}

		internal string Number { get; private set; }
	}
}
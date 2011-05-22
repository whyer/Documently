using NUnit.Framework;

namespace CQRSSample.Specs
{
	public static class StringExtensions
	{
		public static void WillBe(this string x, string what)
		{
			if (x != what)
				throw new AssertionException(string.Format("{0} != {1}", x, what));
		}

	}
}
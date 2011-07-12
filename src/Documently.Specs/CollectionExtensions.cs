using System.Collections;

namespace CQRSSample.Specs
{
	public static class CollectionExtensions
	{
		public static object Last(this ICollection collection)
		{
			return GetLasty(collection);
		}

		public static T Last<T>(this ICollection collection)
		{
			return (T)GetLasty(collection);
		}

		private static object GetLasty(ICollection collection)
		{
			if (collection.Count == 0) return null;

			object last = null;

			var enr = collection.GetEnumerator();
			while (enr.MoveNext())
				last = enr.Current;

			return last;
		}
	}
}
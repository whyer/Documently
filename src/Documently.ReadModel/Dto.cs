using System;

namespace Documently.ReadModel
{
	public abstract class Dto
	{
		public string Id
		{
			get { return GetDtoIdOf(AggregateRootId, GetType()); }
		}

		public Guid AggregateRootId { get; set; }

		public static string GetDtoIdOf<T>(Guid id) where T : Dto
		{
			return GetDtoIdOf(id, typeof (T));
		}

		public static string GetDtoIdOf(Guid id, Type type)
		{
			return type.Name + "/" + id;
		}
	}
}
using System;
using MassTransit;

namespace Documently.ReadModel
{
	public abstract class Dto
	{
		public string Id
		{
			get { return GetDtoIdOf(AggregateRootId, GetType()); }
		}

		public NewId AggregateRootId { get; set; }

		public static string GetDtoIdOf<T>(NewId id) where T : Dto
		{
			return GetDtoIdOf(id, typeof (T));
		}

		public static string GetDtoIdOf(NewId id, Type type)
		{
			return type.Name + "/" + id;
		}
	}
}
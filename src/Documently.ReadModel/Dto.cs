using System;
using MassTransit;

namespace Documently.ReadModel
{
	public abstract class Dto
	{
		public string Id
		{
			get { return GetDtoIdOf(AggregateId, GetType()); }
		}

		public Guid AggregateId { get; set; }

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
using System;
using System.Data;
using System.Linq;
using Automatonymous;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Documently.Sagas.Service.Mapping
{
	/// <summary>
	/// An NHibernate user type for storing a Automatonymous State
	/// </summary>
	[Serializable]
	public class AutomatonymousSagaStateUserType<T> :
		IUserType
		where T : class, StateMachine, new()
	{
		bool IUserType.Equals(object x, object y)
		{
			if (x != null)
				return x.Equals(y);

			if (y != null)
				return y.Equals(x);

			return true;
		}

		public int GetHashCode(object x)
		{
			return x.GetHashCode();
		}

		public object NullSafeGet(IDataReader rs, string[] names, object owner)
		{
			string value = (string)NHibernateUtil.String.NullSafeGet(rs, names);

			JObject o = JObject.Parse(value);

			string name = (string)o.SelectToken("Name");

			var saga = new T();
			State newState = saga.States.FirstOrDefault(s => s.Name == name);

			return newState;
		}

		public void NullSafeSet(IDbCommand cmd, object value, int index)
		{
			if (value == null)
			{
				NHibernateUtil.String.NullSafeSet(cmd, null, index);
				return;
			}

			string jsonValue = JsonConvert.SerializeObject(value);

			NHibernateUtil.String.NullSafeSet(cmd, jsonValue, index);
		}

		public object DeepCopy(object value)
		{
			return value ?? null;
		}

		public object Replace(object original, object target, object owner)
		{
			return original;
		}

		public object Assemble(object cached, object owner)
		{
			return cached;
		}

		public object Disassemble(object value)
		{
			return value;
		}

		public SqlType[] SqlTypes
		{
			get { return new[] { NHibernateUtil.String.SqlType }; }
		}

		public Type ReturnedType
		{
			get { return typeof(State<>); }
		}

		public bool IsMutable
		{
			get { return false; }
		}
	}
}
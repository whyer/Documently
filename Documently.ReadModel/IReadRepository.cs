using System.Linq;

namespace CQRSSample.ReadModel
{
	public interface IReadRepository
	{
		IQueryable<T> GetAll<T>() where T : class;
		T GetById<T>(string id) where T : class;
	}
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.Helpers
{
	public interface IRepository<TEntity>
	{
		Task<IEnumerable<TEntity>> GetAll();
		Task<TEntity> Get(int id);
		Task Add(TEntity entity);
		Task Update(TEntity dbEntity, TEntity entity);
		Task Delete(TEntity entity);
	}
}

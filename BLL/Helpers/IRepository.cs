using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.Helpers
{
	public interface IRepository<TEntity>
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<TEntity> GetAsync(int id);
		Task AddAsync(TEntity entity);
		Task UpdateAsync(TEntity dbEntity, TEntity newEntity);
		Task DeleteAsync(TEntity entity);
	}
}

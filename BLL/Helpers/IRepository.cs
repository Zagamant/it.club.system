using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.Helpers
{
	public interface IRepository<TId, TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<TEntity> GetAsync(TId id);
		Task<TEntity> AddAsync(TEntity entity);
		Task<TEntity>  UpdateAsync(TEntity dbEntity, TEntity newEntity);
		Task<TEntity>  UpdateAsync(TId id, TEntity newEntity);
		Task DeleteAsync(TEntity entity);
		Task DeleteAsync(TId id);
	}
}

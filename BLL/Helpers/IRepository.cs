using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.Helpers
{
	public interface IRepository<TId, TAddModel, TUpdateModel, TModel> 
		where TAddModel : class
		where TUpdateModel : class
		where TModel : class
	{
		Task<IEnumerable<TModel>> GetAllAsync();
		Task<TModel> GetAsync(TId id);
		Task<TModel> AddAsync(TAddModel entity);
		Task<TModel>  UpdateAsync(TId id, TUpdateModel newEntity);
		Task DeleteAsync(TId id);
	}
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.Helpers
{
    public interface IBaseService<TId, TAddModel, TUpdateModel, TModel>
        where TAddModel : class
        where TUpdateModel : class
        where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync(string page,
            string pageSize, string sort = "", string filter = "");

        Task<TModel> GetAsync(TId id);
        Task<TModel> AddAsync(TAddModel entity);
        Task<TModel> UpdateAsync(TId id, TUpdateModel updated);
        Task DeleteAsync(TId id, bool isDelete = false);
        Task<int> Count();
    }
}
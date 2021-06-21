using System.BLL.Models.RoleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.RoleManagement
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleModel>> GetAllAsync(string page = "",
            string pageSize = "", string sort = "", string filter = "");
        Task<RoleModel> GetAsync(int id);
        Task<RoleModel> AddAsync(RoleModel role);
        Task<RoleModel> UpdateAsync(int roleId, RoleModel role);
        Task RemoveAsync(int roleId);
        Task<int> Count();

    }
}
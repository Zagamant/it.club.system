using System.BLL.Models.RoleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClient.Services.RoleManagement
{
	public interface IRoleService
	{
		Task<List<RoleModel>> GetAllAsync();
		Task<RoleModel> GetAsync(int id);
		Task<RoleModel> AddAsync(RoleModel role);
		Task<RoleModel> UpdateAsync(int roleId, RoleModel role);
		Task DeleteAsync(int roleId);

		Task<bool> AddRoleToUser(string username, string roleName);
		Task<bool> RemoveUsersRole(string username, string roleName);

	}
}

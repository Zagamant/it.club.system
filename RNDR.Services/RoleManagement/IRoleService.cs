using System.Threading.Tasks;

namespace System.BLL.RoleManagement
{
	public interface IRoleService
	{
		Task CreateRole(string roleName);
		Task<bool> AddRoleToUser(string username, string roleName);
		Task<bool> RemoveUsersRole(string username, string roleName);

	}
}

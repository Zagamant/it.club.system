using System.BLL.Models.RoleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClient.Services.RoleManagement
{
    public class RoleService : IRoleService
    {
        public RoleService()
        {
        }


        public async Task<List<RoleModel>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<RoleModel> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RoleModel> AddAsync(RoleModel role)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RoleModel> UpdateAsync(int roleId, RoleModel role)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(int roleId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddRoleToUser(string username, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveUsersRole(string username, string roleName)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.BLL.Helpers;
using System.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace System.BLL.RoleManagement
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<Role> _roleManager;
		private readonly UserManager<User> _userManager;

		public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager)
		{
			_roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		}

		public async Task CreateRole(string roleName)
		{
			if (roleName == null) throw new ArgumentNullException(nameof(roleName));

			await _roleManager.CreateAsync(new Role(roleName));
		}

		public async Task<bool> AddRoleToUser(string username, string roleName)
		{
			if(username == null) throw new ArgumentNullException(nameof(username));
			if(roleName == null) throw new ArgumentNullException(nameof(roleName));

			var user = await _userManager.FindByNameAsync(username);
			if (user == null)
			{
				throw new AppException("User with that username is not existed");
			}

			var result = await _userManager.AddToRoleAsync(user, roleName);

			return result.Succeeded;
		}


		public async Task<bool> RemoveUsersRole(string username, string roleName)
		{
			if (username == null) throw new ArgumentNullException(nameof(username));
			if (roleName == null) throw new ArgumentNullException(nameof(roleName));

			var user = await _userManager.FindByNameAsync(username);
			if (user == null)
			{
				throw new AppException("User with that username is not existed");
			}

			var result = await _userManager.RemoveFromRoleAsync(user, roleName);

			return result.Succeeded;
		}
	}
}

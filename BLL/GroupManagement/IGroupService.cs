using System.BLL.Models.GroupManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using System.Threading.Tasks;

namespace System.BLL.GroupManagement
{
	public interface IGroupService
	{
		Task<GroupModel> CreateAsync(GroupModel group);
		Task<IEnumerable<GroupModel>> GetAllAsync();
		Task<GroupModel> GetByIdAsync(int groupId);
		Task<GroupModel> UpdateAsync(int groupId, GroupModel newGroup);
		Task<GroupModel> AddStudentAsync(int groupId, GroupModel user);
		Task<GroupModel> AddStudentAsync(int groupId, int userId);
		Task<GroupModel> RemoveStudentAsync(int groupId, User user);
		Task<GroupModel> RemoveStudentAsync(int groupId, int userId);
		Task RemoveAsync(int groupId, bool isDelete = false);
		Task RemoveAsync(Group group, bool isDelete = false);

	}
}

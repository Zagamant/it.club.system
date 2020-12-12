using System.BLL.Models.GroupManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using System.Threading.Tasks;

namespace System.BLL.GroupManagement
{
	public interface IGroupService
	{
		Task CreateAsync(Group group);
		Task<IEnumerable<Group>> GetAllAsync();
		Task<Group> GetByIdAsync(int groupId);
		Task UpdateAsync(int groupId, Group newGroup);
		Task UpdateAsync(Group group, Group newGroup);
		Task AddStudentAsync(int groupId, User user);
		Task AddStudentAsync(Group group, User user);
		Task RemoveStudentAsync(int groupId, User user);
		Task RemoveStudentAsync(Group group, User user);
		Task RemoveAsync(int groupId, bool isDelete = false);
		Task RemoveAsync(Group group, bool isDelete = false);

	}
}

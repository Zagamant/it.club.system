using System.BLL.Helpers;
using System.BLL.Models.GroupManagement;
using System.Threading.Tasks;

namespace System.BLL.GroupManagement
{
    public interface IGroupService : IBaseService<int, GroupModel, GroupModel, GroupModel>
    {
        Task<GroupModel> AddStudentAsync(int groupId, int userId);
        Task<GroupModel> RemoveStudentAsync(int groupId, int userId);
    }
}
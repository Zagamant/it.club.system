using System.BLL.Models.GroupManagement;
using System.Threading.Tasks;
using BlazorClient.Services.Helpers;
using GroupModel = BlazorClient.Models.Group.GroupModel;

namespace BlazorClient.Services.GroupManagement
{
    public interface IGroupService : IServiceBase<int, GroupModel, GroupModel, GroupModel>
    {
        Task<GroupModel> AddStudentAsync(int groupId, int userId);
        Task<GroupModel> RemoveStudentAsync(int groupId, int userId);
    }
}
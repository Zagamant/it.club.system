using System.BLL.GroupManagement;
using System.BLL.Models.GroupManagement;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GroupsController : BaseController<IGroupService, GroupModel, GroupModel, GroupModel>
    {
        public GroupsController(IGroupService service) : base(service)
        {
        }

        [HttpPut("{groupId}/students/{studentId}")]
        public async Task<ActionResult<GroupModel>> DeleteStudent(int groupId, int studentId) =>
            Ok(await _service.RemoveStudentAsync(groupId, studentId));

        [HttpDelete("{groupId}/students/{studentId}")]
        public async Task<ActionResult<GroupModel>> AddStudent(int groupId, int studentId) =>
            Ok(await _service.AddStudentAsync(groupId, studentId));
    }
}
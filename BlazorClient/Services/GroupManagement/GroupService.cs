using System.BLL.Models.GroupManagement;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorClient.Services.Helpers;
using Newtonsoft.Json;
using GroupModel = BlazorClient.Models.Group.GroupModel;

namespace BlazorClient.Services.GroupManagement
{
    public class GroupService : ServiceBase<int, GroupModel, GroupModel, GroupModel>,
        IGroupService
    {
        public GroupService(HttpClient http) : base(http)
        {
        }

        public async Task<GroupModel> AddStudentAsync(int groupId, int userId)
        {
            var response = await _http.PutAsJsonAsync($"{_url}/AddStudent", new
            {
                groupId,
                userId
            });

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<GroupModel>(responseText);
                return responseObj;
            }

            throw new HttpRequestException("Smth goes wrong in repo");
        }

        public async Task<GroupModel> RemoveStudentAsync(int groupId, int userId)
        {
            var response = await _http.PutAsJsonAsync($"{_url}/RemoveStudent", new
            {
                groupId,
                userId
            });

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<GroupModel>(responseText);
                return responseObj;
            }

            throw new HttpRequestException("Smth goes wrong in repo");
        }
    }
}
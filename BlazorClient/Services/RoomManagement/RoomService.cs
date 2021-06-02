using System.BLL.Models.RoomManagement;
using System.Net.Http;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.RoomManagement
{
    public class RoomService : ServiceBase<int, RoomCreateModel, RoomModel, RoomModel>, IRoomService
    {
        public RoomService(HttpClient http) : base(http)
        {
        }
    }
}
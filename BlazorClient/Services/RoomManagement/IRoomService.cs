using System.BLL.Models.RoomManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.RoomManagement
{
    public interface
        IRoomService : IServiceBase<int, RoomCreateModel, RoomModel, RoomModel>
    {
    }
}
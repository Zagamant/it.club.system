using System.BLL.Models.RoomManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.RoomManagement
{
    public interface
        IRoomService : IRepository<int, RoomCreateModel, RoomModel, RoomModel>
    {
    }
}
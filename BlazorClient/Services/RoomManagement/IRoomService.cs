using System.BLL.Helpers;
using System.BLL.Models.RoomManagement;

namespace System.BLL.RoomManagement
{
    public interface IRoomService : BlazorClient.Services.Helpers.IRepository<int, RoomCreateModel, RoomModel, RoomModel>
    {
    }
}
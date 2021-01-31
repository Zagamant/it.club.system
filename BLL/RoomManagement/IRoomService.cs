using System.BLL.Helpers;
using System.BLL.Models.RoomManagement;

namespace System.BLL.RoomManagement
{
    public interface IRoomService : IRepository<int, RoomCreateModel, RoomModel, RoomModel>
    {
    }
}
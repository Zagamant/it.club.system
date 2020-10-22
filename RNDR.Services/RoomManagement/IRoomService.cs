using System.BLL.Models.RoomManagement;
using System.DAL.Models;
using System.Threading.Tasks;

namespace System.BLL.RoomManagement
{
	public interface IRoomService
	{
		Task Create(RoomModel room);

		Task<Room> GetRoom(RoomSafeModel room);

		Task Update(int roomId, RoomModel newRoom);
		Task Update(RoomSafeModel room, RoomModel newRoom);

		Task Remove(int roomId);
		Task Remove(RoomSafeModel room);

	}
}

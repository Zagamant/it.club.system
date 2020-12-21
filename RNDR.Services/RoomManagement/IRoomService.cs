using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.RoomManagement
{
	public interface IRoomService
	{
		Task Create(Room room);
		Task<IEnumerable<Room>> GetAll();
		Task<Room> Get(Room room);
		Task<Room> Get(int roomId);
		Task Update(int roomId, Room newRoom);
		Task Update(Room room, Room newRoom);
		Task Remove(int roomId);
		Task Remove(Room room);
	}
}

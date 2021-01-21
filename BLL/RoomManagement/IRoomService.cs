using System.BLL.Models.RoomManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.RoomManagement
{
	public interface IRoomService
	{
		Task<Room> CreateAsync(RoomCreate room);
		Task<IEnumerable<Room>> GetAllAsync();
		Task<Room> GetAsync(Room room);
		Task<Room> GetAsync(int roomId);
		Task UpdateAsync(int roomId, Room newRoom);
		Task UpdateAsync(Room room, Room newRoom);
		Task RemoveAsync(int roomId);
		Task RemoveAsync(Room room);
	}
}

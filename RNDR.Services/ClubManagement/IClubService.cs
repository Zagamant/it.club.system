using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.ClubManagement
{
	public interface IClubService
	{
		Task<IEnumerable<Club>> GetAllAsync(string userId);
		Task<Club> GetByIdAsync(int clubId, string userId);
		Task<Club> GetByTitleAsync(string clubTitle, string userId);
		Task<Club> CreateAsync(Club club);
		Task AddRoomAsync(int clubId, Room room, string userId);
		Task AddRoomAsync(int clubId, int roomId, string userId);
		Task AddRoomAsync(Club club, int roomId, string userId);
		Task AddRoomAsync(Club club, Room room, string userId);
		Task RemoveRoomAsync(int clubId, Room room, string userId, bool isDeleteRoom = false);
		Task RemoveRoomAsync(Club club, Room room, string userId, bool isDeleteRoom = false);
		Task<Club> UpdateAsync(int clubId, Club newClub, string userId);
		Task<Club> UpdateAsync(Club club, Club newClub, string userId);
		Task RemoveAsync(int clubId, string userId, bool isDelete = false);
		Task RemoveAsync(Club club, string userId, bool isDelete = false);
	}
}

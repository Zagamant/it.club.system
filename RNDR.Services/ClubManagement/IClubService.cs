using System.BLL.Models.ClubManagement;
using System.BLL.Models.RoomManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.ClubManagement
{
	public interface IClubService
	{
		Task<IEnumerable<ClubModel>> GetAllAsync(string userId);
		Task<ClubModel> GetByIdAsync(int clubId, string userId);
		Task<ClubModel> GetByTitleAsync(string clubTitle, string userId);
		Task<ClubModel> CreateAsync(ClubRegister club);
		Task AddRoomAsync(int clubId, RoomModel room, string userId);
		Task AddRoomAsync(ClubSafeModel club, RoomModel room, string userId);
		Task RemoveRoomAsync(int clubId, RoomModel room, string userId, bool isDeleteRoom = false);
		Task RemoveRoomAsync(ClubSafeModel club, RoomModel room, string userId, bool isDeleteRoom = false);
		Task<ClubModel> UpdateAsync(int clubId, ClubSafeModel newClub, string userId);
		Task<ClubModel> UpdateAsync(Club club, ClubSafeModel newClub, string userId);
		Task RemoveAsync(int clubId, string userId, bool isDelete = false);
		Task RemoveAsync(ClubSafeModel club, string userId, bool isDelete = false);
	}
}

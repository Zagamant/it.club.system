using System.BLL.Models.ClubManagement;
using System.BLL.Models.RoomManagement;
using System.Collections.Generic;
using System.DAL.Models;
using System.Threading.Tasks;

namespace System.BLL.ClubManagement
{
	public interface IClubService
	{
		Task<IEnumerable<ClubModel>> GetAll();
		Task<ClubModel> GetById(int clubId);
		Task<ClubModel> GetByTitle(string clubTitle);
		Task<ClubModel> CreateClub(ClubRegister club);
		Task AddRoom(int clubId, RoomModel room);
		Task AddRoom(ClubSafeModel club, RoomModel room);
		Task RemoveRoom(int clubId, RoomModel room, bool isDeleteRoom = false);
		Task RemoveRoom(ClubSafeModel club, RoomModel room, bool isDeleteRoom = false);
		Task<ClubModel> UpdateClub(int clubId, ClubSafeModel newClub);
		Task<ClubModel> UpdateClub(Club club, ClubSafeModel newClub);
		Task RemoveClub(int clubId, bool isDelete = false);
		Task RemoveClub(ClubSafeModel club, bool isDelete = false);
	}
}

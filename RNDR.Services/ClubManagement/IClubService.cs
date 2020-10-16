using System.Collections.Generic;
using System.DAL.Models;
using System.Threading.Tasks;

namespace System.BLL.ClubManagement
{
	public interface IClubService
	{
		Task<IEnumerable<Club>> GetAll();
		Task<Club> GetById(int clubId);
		Task<Club> CreateClub(Club club);
		Task AddRoom(int clubId, Room room);
		Task AddRoom(Club club, Room room);
		Task RemoveRoom(int clubId, Room room, bool isDeleteRoom = false);
		Task RemoveRoom(Club club, Room room, bool isDeleteRoom = false);
		Task<Club> UpdateClub(int clubId, Club newClub);
		Task<Club> UpdateClub(Club club, Club newClub);

	}
}

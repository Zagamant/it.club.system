using System.Collections.Generic;
using System.DAL.Models;
using System.Threading.Tasks;

namespace System.BLL.ClubManagement
{
	public class ClubService : IClubService
	{
		public async Task<IEnumerable<Club>> GetAll()
		{
			throw new NotImplementedException();
		}

		public async Task<Club> GetById(int clubId)
		{
			throw new NotImplementedException();
		}

		public async Task<Club> CreateClub(Club club)
		{
			throw new NotImplementedException();
		}

		public async Task AddRoom(int clubId, Room room)
		{
			throw new NotImplementedException();
		}

		public async Task AddRoom(Club club, Room room)
		{
			throw new NotImplementedException();
		}

		public async Task RemoveRoom(int clubId, Room room, bool isDeleteRoom = false)
		{
			throw new NotImplementedException();
		}

		public async Task RemoveRoom(Club club, Room room, bool isDeleteRoom = false)
		{
			throw new NotImplementedException();
		}

		public async Task<Club> UpdateClub(int clubId, Club newClub)
		{
			throw new NotImplementedException();
		}

		public async Task<Club> UpdateClub(Club club, Club newClub)
		{
			throw new NotImplementedException();
		}
	}
}

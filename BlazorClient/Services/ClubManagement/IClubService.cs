using System.BLL.Models.ClubManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClient.Services.ClubManagement
{
	public interface IClubService : Helpers.IRepository<int, ClubModel,ClubModel,ClubModel>
	{
		Task<ClubModel> AddRoomAsync(int clubId, int roomId);
		Task<ClubModel> RemoveRoomAsync(int clubId, int roomId);
		Task<IEnumerable<ClubModel>> GetClubsByUser(int userId);
	}
}

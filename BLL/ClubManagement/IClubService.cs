using System.BLL.Helpers;
using System.BLL.Models.ClubManagement;
using System.Threading.Tasks;

namespace System.BLL.ClubManagement
{
	public interface IClubService : IRepository<int, ClubModel,ClubModel,ClubModel>
	{
		Task<ClubModel> GetByTitleAsync(string clubTitle, string userId);
		Task<ClubModel> AddRoomAsync(int clubId, int roomId, string userId);
		Task<ClubModel> RemoveRoomAsync(int clubId, int roomId, string userId, bool isDeleteRoom = false);
	}
}

using System.BLL.Models.ClubManagement;
using System.Threading.Tasks;

namespace BlazorClient.Services.ClubManagement
{
	public interface IClubService : BlazorClient.Services.Helpers.IRepository<int, ClubModel,ClubModel,ClubModel>
	{
		Task<ClubModel> AddRoomAsync(int clubId, int roomId);
		Task<ClubModel> RemoveRoomAsync(int clubId, int roomId);
	}
}

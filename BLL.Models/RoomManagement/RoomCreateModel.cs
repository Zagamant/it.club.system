using System.BLL.Models.Helpers;

namespace System.BLL.Models.RoomManagement
{
	public class RoomCreateModel : BaseModel
	{
		public int ClubId { get; set; }
		public int Capacity { get; set; }
		public string RoomNumber { get; set; }
		public string About { get; set; }
		
	}
}
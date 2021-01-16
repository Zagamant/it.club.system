namespace System.BLL.Models.RoomManagement
{
	public class RoomCreate
	{
		public int Id { get; set; }
		public int ClubId { get; set; }
		public int Capacity { get; set; }
		public string RoomNumber { get; set; }
		public string About { get; set; }
		
	}
}
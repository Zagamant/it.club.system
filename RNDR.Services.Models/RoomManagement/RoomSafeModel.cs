namespace System.BLL.Models.RoomManagement
{
	public class RoomSafeModel
	{
		public virtual string ClubTitle { get; set; }
		public virtual string RoomNumber { get; set; }
		public virtual int Capacity { get; set; }
		public virtual string About { get; set; }

	}
}
using System.Collections.Generic;
using System.DAL.Entities.Enums;

namespace System.DAL.Entities
{
	public class Room: BaseEntity
	{
		public int ClubId { get; set; }
		public virtual Club Club { get; set; }
		public int Capacity { get; set; }
		public string RoomNumber { get; set; }
		public string About { get; set; }
		public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
		public virtual RoomStatus Status { get; set; }

	}
}

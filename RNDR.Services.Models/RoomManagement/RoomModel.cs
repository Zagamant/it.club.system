using System.Collections.Generic;
using System.DAL.Enums;
using System.DAL.Models;

namespace System.BLL.Models.RoomManagement
{
	public class RoomModel
	{
		public virtual Club Club { get; set; }
		public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
		public virtual int Capacity { get; set; }
		public virtual string RoomNumber { get; set; }
		public virtual string About { get; set; }
		public virtual RoomStatus Status { get; set; }

	}
}
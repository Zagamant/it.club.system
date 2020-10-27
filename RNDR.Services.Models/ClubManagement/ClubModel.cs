using System.Collections.Generic;
using System.DAL.Entities;
using System.DAL.Entities.Enums;

namespace System.BLL.Models.ClubManagement
{
	public class ClubModel
	{
		public virtual string Title { get; set; }
		public virtual Address Address { get; set; }
		public virtual ClubStatus Status { get; set; }
		public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
	}
}

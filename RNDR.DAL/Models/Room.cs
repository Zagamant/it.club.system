using System.Collections;
using System.Collections.Generic;

namespace RNDR.DAL.Models
{
	public class Room
	{
		public virtual int Id { get; set; }
		public virtual Club Club { get; set; }
		public virtual ICollection<Group> Groups { get; set; }
		public virtual int Capacity { get; set; }
		public virtual string RoomNumber { get; set; }
		public virtual string About { get; set; }
		public virtual bool DeleteStatus { get; set; }

	}
}

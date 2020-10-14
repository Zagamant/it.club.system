using System.Collections.Generic;

namespace RNDR.DAL.Models
{
	public class Club
	{
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual Address AddressId { get; set; }
		public virtual bool DeleteStatus{ get; set; }
		public virtual ICollection<Room> Rooms{ get; set; }

	}
}

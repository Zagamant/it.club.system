using System.Collections.Generic;

namespace RNDR.DAL.Models
{
	public class Course
	{
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string About { get; set; }
		public virtual string ManualLink { get; set; }
		public virtual ICollection<Group> Groups { get; set; }

	}
}

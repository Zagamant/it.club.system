using System.Collections.Generic;

namespace System.DAL.Entities
{
	public class Course
	{
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string About { get; set; }
		public virtual string ManualLink { get; set; }
		public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

	}
}

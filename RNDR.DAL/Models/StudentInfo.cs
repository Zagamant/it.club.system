using System.Collections.Generic;

namespace System.DAL.Models
{
	public class StudentInfo : InfoBase
	{
		public virtual string ParentContact { get; set; }
		public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
		public virtual ICollection<Course> CoursesPassed { get; set; } = new List<Course>();
		public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();
	}
}

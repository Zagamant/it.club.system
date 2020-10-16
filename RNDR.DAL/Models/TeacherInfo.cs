using System.Collections.Generic;

namespace System.DAL.Models
{
	public class TeacherInfo : InfoBase
	{
		public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
	}
}

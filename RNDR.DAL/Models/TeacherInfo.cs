using System.Collections.Generic;

namespace System.DAL.Models
{
	public class TeacherInfo
	{
		public virtual User Teacher { get; set; }
		public virtual string Name { get; set; }
		public virtual string Surname { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual string PhoneNumber { get; set; }
		public virtual string Skype { get; set; }
		public virtual Address Address { get; set; }
		public virtual string Photo { get; set; }
		public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
	}
}

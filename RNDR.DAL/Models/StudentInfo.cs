﻿using System.Collections.Generic;

namespace System.DAL.Models
{
	public class StudentInfo
	{
		public virtual User Student { get; set; }
		public virtual string Phone { get; set; }
		public virtual Address Address { get; set; }
		public virtual string Name { get; set; }
		public virtual string Surname { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual DateTime BirthDay { get; set; }
		public virtual string ParentContact { get; set; }
		public virtual string Skype { get; set; }
		public virtual string AdditionalInfo { get; set; }
		public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
		public virtual ICollection<Course> CoursesPassed { get; set; } = new List<Course>();

	}
}

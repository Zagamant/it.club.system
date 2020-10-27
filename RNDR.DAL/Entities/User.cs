using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace System.DAL.Entities
{
	/// <summary>
	/// Represent user in database.
	/// </summary>
	public class User : IdentityUser<int>
	{
		public virtual string Name { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual string Surname { get; set; }
		public virtual DateTime BirthDay { get; set; }
		public virtual Address Address { get; set; }
		public virtual string AdditionalInfo { get; set; }
		public virtual IEnumerable<Photo> Photos { get; set; } = new List<Photo>();
		public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
		public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
		public virtual ICollection<Course> CoursesPassed { get; set; } = new List<Course>();

	}
}
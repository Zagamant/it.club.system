using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace System.DAL.Entities
{
	/// <summary>
	/// Represent user in database.
	/// </summary>
	public class User : IdentityUser<int>
	{
		public string Name { get; set; }
		public string MiddleName { get; set; }
		public string Surname { get; set; }
		public DateTime BirthDay { get; set; }
		public virtual Address Address { get; set; }
		public string AdditionalInfo { get; set; }
		public virtual IEnumerable<Photo> Photos { get; set; }
		public virtual ICollection<Contact> Contacts { get; set; }
		public virtual ICollection<Group> Groups { get; set; }
		public virtual ICollection<Course> CoursesPassed { get; set; }
	}
}
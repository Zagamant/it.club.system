using System.Collections.Generic;
using System.DAL.Entities;

namespace System.BLL.Models.CourseManagement
{
	public class CourseModel
	{
		public virtual string Title { get; set; }
		public virtual string About { get; set; }
		public virtual string ManualLink { get; set; }
		public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
	}
}

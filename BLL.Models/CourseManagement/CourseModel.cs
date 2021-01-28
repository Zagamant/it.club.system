using System.Collections.Generic;
using System.DAL.Entities;

namespace System.BLL.Models.CourseManagement
{
	public class CourseModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string About { get; set; }
		public string ManualLink { get; set; }
		public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
	}
}

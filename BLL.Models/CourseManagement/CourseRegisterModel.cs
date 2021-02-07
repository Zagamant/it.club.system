using System.BLL.Models.Helpers;

namespace System.BLL.Models.CourseManagement
{
	public class CourseRegisterModel : BaseModel
	{
		public virtual string Title { get; set; }
		public virtual string About { get; set; }
		public virtual string ManualLink { get; set; }
	}
}

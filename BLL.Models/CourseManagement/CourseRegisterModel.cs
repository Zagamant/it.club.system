using System.BLL.Models.Helpers;

namespace System.BLL.Models.CourseManagement
{
	public class CourseRegisterModel : BaseModel
	{
		public string Title { get; set; }
		public string About { get; set; }
		public string ManualLink { get; set; }
	}
}

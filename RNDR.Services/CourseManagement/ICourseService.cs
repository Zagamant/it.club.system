using System.BLL.Models.CourseManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.CourseManagement
{
	public interface ICourseService
	{
		Task Create(CourseRegisterModel room);
		Task<IEnumerable<CourseModel>> GetAllCourses();
		Task<CourseModel> GetCourse(CourseModel room);
		Task Update(int courseId, CourseModel newCourse);
		Task Update(CourseModel course, CourseModel newCourse);
		Task Remove(int courseId);
		Task Remove(CourseModel room);
	}
}

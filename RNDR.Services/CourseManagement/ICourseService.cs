using System.BLL.Models.CourseManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.CourseManagement
{
	public interface ICourseService
	{
		Task CreateAsync(CourseRegisterModel course);
		Task<IEnumerable<CourseModel>> GetAllAsync();
		Task<CourseModel> GetAsync(CourseModel course);
		Task<CourseModel> GetAsync(int id);
		Task UpdateAsync(int courseId, CourseModel newCourse);
		Task UpdateAsync(CourseModel course, CourseModel newCourse);
		Task RemoveAsync(int courseId);
		Task RemoveAsync(CourseModel course);
	}
}


using System;
using System.Collections.Generic;
using System.DAL.Models;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.CourseManagement
{
	public interface ICourseService
	{
		Task Create(Course room);
		Task<Course> GetCourse(CourseSafeModel room);
		Task Update(int roomId, CourseModel newRoom);
		Task Update(CourseSafeModel room, CourseModel newRoom);
		Task Remove(int courseId);
		Task Remove(CourseSafeModel room);
	}
}

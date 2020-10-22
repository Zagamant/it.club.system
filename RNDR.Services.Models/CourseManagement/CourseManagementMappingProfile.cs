using AutoMapper;
using System.DAL.Models;

namespace System.BLL.Models.CourseManagement
{
	class CourseManagementMappingProfile : Profile
	{
		public CourseManagementMappingProfile()
		{
			CreateMap<Course, CourseModel>();
			CreateMap<Course, CourseSafeModel>();
		}
	}
}

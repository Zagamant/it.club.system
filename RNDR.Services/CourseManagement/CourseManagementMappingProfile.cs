using System.BLL.Models.CourseManagement;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.CourseManagement
{
	public class CourseManagementMappingProfile : Profile
	{
		public CourseManagementMappingProfile()
		{
			CreateMap<Course, CourseModel>();
			CreateMap<CourseModel, Course>();
			CreateMap<Course, CourseRegisterModel>();
		}
	}
}

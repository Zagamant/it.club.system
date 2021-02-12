using System.BLL.Helpers;
using System.BLL.Models.CourseManagement;
using System.DAL;
using System.DAL.Entities;
using AutoMapper;

namespace System.BLL.CourseManagement
{
    public class CourseService : Repository<int, Course, CourseRegisterModel, CourseModel, CourseModel>, ICourseService
    {
        public CourseService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _table = _context.Courses;
        }
    }
}
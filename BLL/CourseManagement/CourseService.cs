using System.BLL.Helpers;
using System.BLL.Models.CourseManagement;
using System.DAL;
using System.DAL.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace System.BLL.CourseManagement
{
    public class CourseService : Repository<int, Course, CourseRegisterModel, CourseModel, CourseModel>, ICourseService
    {
        public CourseService(DataContext context, IMapper mapper, ILogger<CourseService> logger) : base(context, mapper, logger)
        {
            _table = _context.Courses;
        }
    }
}
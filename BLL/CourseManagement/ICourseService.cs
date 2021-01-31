using System.BLL.Helpers;
using System.BLL.Models.CourseManagement;

namespace System.BLL.CourseManagement
{
    public interface ICourseService : IRepository<int, CourseRegisterModel, CourseModel, CourseModel>
    {
    }
}
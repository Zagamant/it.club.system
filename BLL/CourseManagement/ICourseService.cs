using System.BLL.Helpers;
using System.BLL.Models.CourseManagement;

namespace System.BLL.CourseManagement
{
    public interface ICourseService : IBaseService<int, CourseRegisterModel, CourseModel, CourseModel>
    {
    }
}
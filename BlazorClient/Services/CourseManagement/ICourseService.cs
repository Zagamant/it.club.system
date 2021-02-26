using System.BLL.Models.CourseManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.CourseManagement
{
    public interface ICourseService : IRepository<int, CourseRegisterModel, CourseModel, CourseModel>
    {
    }
}
using System.BLL.Models.CourseManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.CourseManagement
{
    public interface ICourseService : IServiceBase<int, CourseRegisterModel, CourseModel, CourseModel>
    {
    }
}
using System.BLL.Models.CourseManagement;
using System.DAL;
using System.Net.Http;
using AutoMapper;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.CourseManagement
{
    public class CourseService : Repository<int, CourseRegisterModel, CourseModel, CourseModel>,
        ICourseService
    {
        public CourseService(HttpClient http) : base(http)
        {
        }
    }
}
using System.BLL.CourseManagement;
using System.BLL.Models.CourseManagement;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CoursesController : BaseController<ICourseService, CourseRegisterModel, CourseModel, CourseModel>
    {
        public CoursesController(ICourseService service) : base(service)
        {
        }
    }
}

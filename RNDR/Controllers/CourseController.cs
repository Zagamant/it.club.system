using System.API.Helpers;
using System.BLL.CourseManagement;
using System.BLL.GroupManagement;
using System.BLL.Models.CourseManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CoursesController(ICourseService courseService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IEnumerable<CourseModel>> Get() => await _courseService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<CourseModel> GetAsync(int id) => await _courseService.GetAsync(id);

        [HttpPost]
        public async Task Post([FromBody] CourseRegisterModel value) => await _courseService.CreateAsync(value);

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CourseModel value) => await _courseService.UpdateAsync(id, value);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await _courseService.RemoveAsync(id);
    }
}
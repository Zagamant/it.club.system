using System.BLL.Models.RoleManagement;
using System.BLL.RoleManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Int32;

namespace System.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleModel>>> Get()
        {
           var result = await _service.GetAllAsync();
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Add("Content-Range", $"{result.Count}");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> GetAsync(int id) => Ok(await _service.GetAsync(id));

        [HttpPost]
        public async Task<ActionResult<RoleModel>> Post([FromBody] RoleModel value) =>
            StatusCode(StatusCodes.Status201Created, await _service.AddAsync(value));

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleModel>> Put(int id, [FromBody] RoleModel value) =>
            Ok(await _service.UpdateAsync(id, value));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
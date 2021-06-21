using System.BLL.Models.RoleManagement;
using System.BLL.RoleManagement;
using System.Collections.Generic;
using System.Linq;
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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleModel>>> Get(string page = "",
            string pageSize = "", string sort = "", string filter = "")
        {
            var result = await _service.GetAllAsync(page, pageSize, sort, filter);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Add("Content-Range", $"{await _service.Count()}");
            
            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<RoleModel>> GetAsync(int Id)
        {
            return Ok(await _service.GetAsync(Id));
        }

        [HttpPost]
        public async Task<ActionResult<RoleModel>> Post([FromBody] RoleModel value)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, await _service.AddAsync(value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RoleModel>> Put(int id, [FromBody] RoleModel value)
        {
            try
            {
                return Ok(await _service.UpdateAsync(id, value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("/user/{userId:int}")]
        public async Task<ActionResult<RoleModel>> GetRolesByUserIdAsync(int userId)
        {
            return Ok(await _service.GetAsync(userId));
        }
    }
}
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
        public async Task<ActionResult<IEnumerable<RoleModel>>> Get()
        {
           var result = await _service.GetAllAsync();
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Add("Content-Range", $"{result.Count()}");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> GetAsync(int id)
        {
            try
            {
                return Ok(await _service.GetAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpPut("{id}")]
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
    }
}
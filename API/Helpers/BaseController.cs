using System.BLL.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Helpers
{
    [ApiController]
    public class BaseController<TService, TAddModel, TUpdateModel, TModel> : ControllerBase
        where TAddModel : class, new()
        where TUpdateModel : class, new()
        where TModel : class, new()
        where TService : IBaseService<int, TAddModel, TUpdateModel, TModel>
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> Get(string page = "",
            string pageSize = "", string sort = "", string filter = "")
        {
            var result = await _service.GetAllAsync(page, pageSize, sort, filter);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Add("Content-Range", $"{await _service.Count()}");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> GetAsync(int id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<TModel>> Post([FromBody] TAddModel value)
        {
            return StatusCode(StatusCodes.Status201Created, await _service.AddAsync(value));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TModel>> Put(int id, [FromBody] TUpdateModel value)
        {
            return Ok(await _service.UpdateAsync(id, value));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, [FromQuery] bool isDelete = false)
        {
            await _service.DeleteAsync(id, isDelete);
            return NoContent();
        }
    }
}
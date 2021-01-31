using System.BLL.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [ApiController]
    public class BaseController<TService,TAddModel, TUpdate, TModel> : ControllerBase 
        where TAddModel : class, new()
        where TUpdate : class, new()
        where TModel : class, new()
        where TService : IRepository<int, TAddModel, TUpdate,TModel>
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> GetAsync(int id) => Ok(await _service.GetAsync(id));

        [HttpPost]
        public async Task<ActionResult<TModel>> Post([FromBody] TAddModel value) => StatusCode(StatusCodes.Status201Created, await _service.AddAsync(value));

        [HttpPut("{id}")]
        public async Task<ActionResult<TModel>> Put(int id, [FromBody] TUpdate value) => Ok(await _service.UpdateAsync(id, value));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
using System.BLL.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace System.API.Helpers
{
    [ApiController]
    public class BaseController<TService, TAddModel, TUpdateModel, TModel> : ControllerBase
        where TAddModel : class, new()
        where TUpdateModel : class, new()
        where TModel : class, new()
        where TService : IRepository<int, TAddModel, TUpdateModel, TModel>
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TModel>>> Get(string filter = "", string range = "",
            string sort = "")
        {
            var from = 0;
            var to = 0;
            if (!string.IsNullOrEmpty(range))
            {
                var rangeVal = JsonConvert.DeserializeObject<List<int>>(range);
                from = rangeVal.First();
                to = rangeVal.Last();
            }

            var result = await _service.GetAllAsync(filter, range, sort);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Add("Content-Range", $"{typeof(TModel).Name.ToLower()} {from}-{to}/{result.Count()}");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TModel>> GetAsync(int id) => Ok(await _service.GetAsync(id));

        [HttpPost]
        public async Task<ActionResult<TModel>> Post([FromBody] TAddModel value) =>
            StatusCode(StatusCodes.Status201Created, await _service.AddAsync(value));

        [HttpPut("{id}")]
        public async Task<ActionResult<TModel>> Put(int id, [FromBody] TUpdateModel value) =>
            Ok(await _service.UpdateAsync(id, value));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
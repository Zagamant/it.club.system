using System.BLL.CostManagement;
using System.BLL.Models.CostsManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CostsController : ControllerBase
    {
        private readonly ICostsService _costsService;

        public CostsController(ICostsService costsService)
        {
            _costsService = costsService ?? throw new ArgumentNullException(nameof(costsService));
        }

        [HttpGet]
        public async Task<IEnumerable<CostsModel>> Get() => await _costsService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<CostsModel> GetAsync(int id) => await _costsService.GetAsync(id);

        [HttpPost]
        public async Task Post([FromBody] CostsModel value) => await _costsService.AddAsync(value);

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CostsModel value) => await _costsService.UpdateAsync(id, value);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await _costsService.DeleteAsync(id);
    }
}
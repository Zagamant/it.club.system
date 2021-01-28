using System.BLL.CostManagement;
using System.BLL.EventManagement;
using System.BLL.Models.CostsManagement;
using System.BLL.Models.EventManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventModel>>> Get() => Ok(await _eventService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<EventModel>> GetAsync(int id) => Ok(await _eventService.GetAsync(id));

        [HttpPost]
        public async Task<ActionResult<EventModel>> Post([FromBody] EventModel value) => StatusCode(StatusCodes.Status201Created, await _eventService.AddAsync(value));

        [HttpPut("{id}")]
        public async Task<ActionResult<EventModel>> Put(int id, [FromBody] EventModel value) => Ok(await _eventService.UpdateAsync(id, value));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _eventService.DeleteAsync(id);
            return NoContent();
        }
    }
}
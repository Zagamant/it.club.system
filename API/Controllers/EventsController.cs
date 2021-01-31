using System.BLL.EventManagement;
using System.BLL.Models.EventManagement;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventsController : BaseController<IEventService, EventModel, EventModel, EventModel>
    {
        public EventsController(IEventService service) : base(service)
        {
        }
    }
}

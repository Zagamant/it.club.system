using System.BLL.Models.RoomManagement;
using System.BLL.RoomManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoomsController : BaseController<IRoomService, RoomCreateModel, RoomModel, RoomModel>
    {
        public RoomsController(IRoomService service) : base(service)
        {
        }
    }
}

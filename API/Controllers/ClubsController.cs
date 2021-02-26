using System.API.Helpers;
using System.BLL.ClubManagement;
using System.BLL.Models.ClubManagement;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClubsController : BaseController<IClubService, ClubModel, ClubModel, ClubModel>
    {
        public ClubsController(IClubService service) : base(service)
        {
        }
        
        // [HttpGet]
        // public async Task<IEnumerable<Club>> GetAllAsync()
        // {
        //     var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     return await _clubService.GetAllAsync(userId);
        // }
        //
        // [HttpGet("{id}")]
        // public async Task<Club> GetAsync(int id) => await _clubService.GetAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
        //
        // [HttpPost]
        // public async Task<ActionResult<Club>> AddAsync([FromBody] Club club) => StatusCode(StatusCodes.Status201Created, await _clubService.CreateAsync(club));
        //
        // [HttpPut("{id}")]
        // // [Authorize(Roles = "main_admin,admin")]
        // public async Task<IActionResult> UpdateAsync(int id, [FromBody] ClubModel club) =>
        //     Ok(await _clubService.UpdateAsync(club.Id, club, User.FindFirstValue(ClaimTypes.Name)));
        //
        // [HttpDelete("{id}")]
        // // [Authorize(Roles = "main_admin,admin")]
        // public async Task<IActionResult> DeleteAsync(int id)
        // {
        //     await _clubService.DeleteAsync(id, User.FindFirstValue(ClaimTypes.Name));
        //     return NoContent();
        // }
        //
        
        [HttpPut("AddRoom")]
        // [Authorize(Roles = "main_admin,admin")]
        public async Task<ActionResult<ClubModel>> AddRoom([FromBody] int clubId, int roomId) => Ok(await _service.AddRoomAsync(clubId, roomId, User.FindFirstValue(ClaimTypes.Name)));
        
        [HttpPut("RemoveRoom")]
        // [Authorize(Roles = "main_admin,admin")]
        public async Task<ActionResult<ClubModel>> RemoveRoom([FromBody] int clubId, int roomId)
        {
            return await _service.RemoveRoomAsync(clubId, roomId, User.FindFirstValue(ClaimTypes.Name));
        }
    }
}
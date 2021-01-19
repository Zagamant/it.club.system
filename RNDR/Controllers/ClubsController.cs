using Microsoft.AspNetCore.Mvc;
using System.API.Helpers;
using System.BLL.ClubManagement;
using System.BLL.Models.ClubManagement;
using System.BLL.RoomManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;


namespace System.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly IClubService _clubService;
        private readonly IRoomService _roomService;

        private readonly IMapper _mapper;

        public ClubsController(
            IClubService clubService,
            IMapper mapper,
            IOptions<AppSettings> appSettings, IRoomService roomService)
        {
            _clubService = clubService ?? throw new ArgumentNullException(nameof(clubService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IEnumerable<Club>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _clubService.GetAllAsync(userId);
        }

        [HttpGet("{id}")]
        public async Task<Club> Get(int id)
        {
            return await _clubService.GetByIdAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "main_admin,admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ClubModel club) =>
            Ok(await _clubService.UpdateAsync(club.Id, club, User.FindFirstValue(ClaimTypes.Name)));

        [HttpPost]
        public async Task<ActionResult<Club>> Create([FromBody] Club club)
        {
            var newClub = await _clubService.CreateAsync(club);
            return StatusCode(StatusCodes.Status201Created, newClub);
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "main_admin,admin")]
        public async Task<IActionResult> Delete([FromBody] Club club)
        {
            await _clubService.RemoveAsync(club, User.FindFirstValue(ClaimTypes.Name));
            return NoContent();
        }

        [HttpPut("AddRoom")]
        // [Authorize(Roles = "main_admin,admin")]
        public async Task<IActionResult> AddRoom([FromBody] int clubId, int roomId)
        {
            await _clubService.AddRoomAsync(clubId, roomId, User.FindFirstValue(ClaimTypes.Name));
            return Ok();
        }

        [HttpPut("RemoveRoom")]
        // [Authorize(Roles = "main_admin,admin")]
        public async Task<ActionResult> RemoveRoom([FromBody] int clubId, int roomId)
        {
            var room = await _roomService.GetAsync(roomId);
            await _clubService.RemoveRoomAsync(clubId, roomId, User.FindFirstValue(ClaimTypes.Name));
            return Ok();
        }
    }
}
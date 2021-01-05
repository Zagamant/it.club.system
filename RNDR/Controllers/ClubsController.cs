using Microsoft.AspNetCore.Mvc;
using System.API.Helpers;
using System.BLL.ClubManagement;
using System.BLL.RoomManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;


namespace System.API.Controllers
{
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

		// GET: api/<ClubsController>
		[HttpGet]
		public async Task<IEnumerable<Club>> Get() => await _clubService.GetAllAsync(User.FindFirstValue(ClaimTypes.Name));

		// GET api/<ClubsController>/5
		[HttpGet("{id}")]
		public async Task<Club> Get(int id) => await _clubService.GetByIdAsync(id, User.FindFirstValue(ClaimTypes.Name));

		// PUT api/<ClubsController>
		[HttpPut("{id}")]
		// [Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> Update(int id, [FromBody] Club club) => Ok(await _clubService.UpdateAsync(club.Id, club, User.FindFirstValue(ClaimTypes.Name);

		// POST api/<ClubsController>/5
		[HttpPost]
		public async Task<ActionResult> Create([FromBody] Club club)
		{
			var newClub = await _clubService.CreateAsync(club);
			return Ok(newClub);
		}

		// DELETE api/<ClubsController>/5
		[HttpDelete("{id}")]
		// [Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> Delete([FromBody] Club club)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);

			await _clubService.RemoveAsync(club, userId);
			return Ok();
		}

		[HttpPut("AddRoom")]
		// [Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> AddRoom([FromBody] int clubId, int roomId)
		{
			await _clubService.AddRoomAsync(clubId, roomId, User.FindFirstValue(ClaimTypes.Name));
			return Ok();
		}

		[HttpPut("RemoveRoom")]
		// [Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> RemoveRoom([FromBody] int clubId, int roomId)
		{
			var room = await _roomService.Get(roomId);
			await _clubService.RemoveRoomAsync(clubId, roomId, User.FindFirstValue(ClaimTypes.Name));
			return Ok();
		}
	}
}

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
		public async Task<IEnumerable<Club>> Get()
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);
			return await _clubService.GetAllAsync(userId);
		}

		// GET api/<ClubsController>/5
		[HttpGet("{id}")]
		public async Task<Club> Get(int id)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);
			return await _clubService.GetByIdAsync(id, userId);
		}

		// POST api/<ClubsController>
		[HttpPost]
		[Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> ChangeTitle([FromBody] Club club, string title)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);

			var checkClub = await _clubService.GetByTitleAsync(title, userId);
			
			if (checkClub != null) throw new Exception("Club wit that title already exist");

			club.Title = title;

			await _clubService.UpdateAsync(club.Id, new Club{Title = title}, userId);

			return Ok();
		}

		// PUT api/<ClubsController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] Club clubik)
		{
			var club = await _clubService.CreateAsync(clubik);
			return Ok(club);
		}

		// DELETE api/<ClubsController>/5
		[HttpDelete("{id}")]
		[Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> Delete([FromBody] Club clubik)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);

			await _clubService.RemoveAsync(clubik, userId);
			return Ok();
		}

		[HttpPost("AddRoom")]
		[Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> AddRoom([FromBody] int clubId, int roomId)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);

			await _clubService.AddRoomAsync(clubId, roomId, userId);
			return Ok();
		}

		[HttpPost("RemoveRoom")]
		[Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> RemoveRoom([FromBody] int clubId, int roomId)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);
			var room = await _roomService.Get(roomId);
			await _clubService.RemoveRoomAsync(clubId, room, userId);
			return Ok();
		}
	}
}

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
			try
			{

				var userId = User.FindFirstValue(ClaimTypes.Name);
				var asd = await _clubService.GetAllAsync(userId);
				return asd;

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
		}

		// GET api/<ClubsController>/5
		[HttpGet("{id}")]
		public async Task<Club> Get(int id)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);
			return await _clubService.GetByIdAsync(id, userId);
		}

		// PUT api/<ClubsController>
		[HttpPut("{id}")]
		// [Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> Update(int id, [FromBody] Club club)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);

			var checkClub = await _clubService.GetByIdAsync(id, userId);

			if (checkClub == null) throw new ArgumentException("Club not exist");

			club.Id = id;

			await _clubService.UpdateAsync(club.Id, club, userId);

			return Ok();
		}

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
			var userId = User.FindFirstValue(ClaimTypes.Name);

			await _clubService.AddRoomAsync(clubId, roomId, userId);
			return Ok();
		}

		[HttpPut("RemoveRoom")]
		// [Authorize(Roles = "main_admin,admin")]
		public async Task<ActionResult> RemoveRoom([FromBody] int clubId, int roomId)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);
			var room = await _roomService.Get(roomId);
			await _clubService.RemoveRoomAsync(clubId, room, userId);
			return Ok();
		}
	}
}

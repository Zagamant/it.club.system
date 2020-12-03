using Microsoft.AspNetCore.Mvc;
using System.API.Helpers;
using System.BLL.ClubManagement;
using System.BLL.Models.ClubManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;


namespace System.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClubsController : ControllerBase
	{

		private readonly IClubService _clubService;
		private readonly IMapper _mapper;
		private readonly IOptions<AppSettings> _appSettings;

		public ClubsController(
			IClubService clubService,
			IMapper mapper,
			IOptions<AppSettings> appSettings)
		{
			_clubService = clubService;
			_mapper = mapper;
			_appSettings = appSettings;
		}

		// GET: api/<ClubsController>
		[HttpGet]
		public async Task<IEnumerable<ClubModel>> Get()
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);
			return await _clubService.GetAllAsync(userId);
		}

		// GET api/<ClubsController>/5
		[HttpGet("{id}")]
		public async Task<ClubModel> Get(int id)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);
			return await _clubService.GetByIdAsync(id, userId);
		}

		// POST api/<ClubsController>
		[HttpPost]
		public async Task<ActionResult> ChangeTitle([FromBody] Club club, string title)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);

			await _clubService.UpdateAsync(club.Id, new ClubSafeModel{Title = title}, userId);

			return Ok();
		}

		// PUT api/<ClubsController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] ClubRegister clubik)
		{
			var club = await _clubService.CreateAsync(clubik);
			return Ok(club);
		}

		// DELETE api/<ClubsController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete([FromBody] ClubSafeModel clubik)
		{
			var userId = User.FindFirstValue(ClaimTypes.Name);

			await _clubService.RemoveAsync(clubik, userId);
			return Ok();
		}
	}
}

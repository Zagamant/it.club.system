using Microsoft.AspNetCore.Mvc;
using System;
using System.API.Helpers;
using System.BLL.ClubManagement;
using System.BLL.GroupManagement;
using System.BLL.Models.GroupManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace System.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class GroupsController : ControllerBase
	{
		private readonly IGroupService _groupService;
		private readonly IMapper _mapper;

		public GroupsController(IGroupService groupService, IMapper mapper, IOptions<AppSettings> appSettings)
		{
			_groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		// GET: api/<GroupsController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GroupModel>>> GetAll() => Ok(await _groupService.GetAllAsync());

		// GET api/<GroupsController>/5
		[HttpGet("{id}")]
		public async Task<GroupModel> Get(int id) => await _groupService.GetByIdAsync(id);

		// POST api/<GroupsController>
		[HttpPost]
		public async Task<ActionResult<GroupModel>> Post([FromBody] GroupModel value)
		{
			return StatusCode(StatusCodes.Status201Created, await _groupService.CreateAsync(value));
		}

		// PUT api/<GroupsController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<GroupModel>> Put(int id, [FromBody] GroupModel value) => Ok(await _groupService.UpdateAsync(id, value));

		// DELETE api/<GroupsController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			await _groupService.RemoveAsync(id);
			return NoContent();
		}

		[HttpPut("{groupId}/students/{studentId}")]
		public async Task<ActionResult<GroupModel>> DeleteStudent(int groupId, int studentId) => Ok(await _groupService.RemoveStudentAsync(groupId, studentId));

		[HttpDelete("{groupId}/students/{studentId}")]
		public async Task<ActionResult<GroupModel>> AddStudent(int groupId, int studentId) => Ok(await _groupService.AddStudentAsync(groupId, studentId));

	}
}

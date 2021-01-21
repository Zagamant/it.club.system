using Microsoft.AspNetCore.Mvc;
using System;
using System.API.Helpers;
using System.BLL.ClubManagement;
using System.BLL.GroupManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
		public async Task<IEnumerable<Group>> Get() => await _groupService.GetAllAsync();

		// GET api/<GroupsController>/5
		[HttpGet("{id}")]
		public async Task<Group> GetAsync(int id) => await _groupService.GetByIdAsync(id);

		// POST api/<GroupsController>
		[HttpPost]
		public async Task Post([FromBody] Group value) => await _groupService.CreateAsync(value);

		// PUT api/<GroupsController>/5
		[HttpPut("{id}")]
		public async Task Put(int id, [FromBody] Group value) => await _groupService.CreateAsync(value);

		// DELETE api/<GroupsController>/5
		[HttpDelete("{id}")]
		public async Task Delete(int id) => await _groupService.RemoveAsync(id);
		
		[HttpPut("{groupId}/students/{studentId}")]
		public async Task DeleteStudent(int groupId, int studentId) => await _groupService.RemoveStudentAsync(groupId, studentId);

		[HttpDelete("{groupId}/students/{studentId}")]
		public async Task AddStudent(int groupId, int studentId) => await _groupService.AddStudentAsync(groupId, studentId);

	}
}

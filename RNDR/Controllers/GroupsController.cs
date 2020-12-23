﻿using Microsoft.AspNetCore.Mvc;
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
		private readonly IOptions<AppSettings> _appSettings;

		public GroupsController(IGroupService groupService, IMapper mapper, IOptions<AppSettings> appSettings)
		{
			_groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
		}


		// GET: api/<GroupsController>
		[HttpGet]
		public async Task<IEnumerable<Group>> Get()
		{
			return await _groupService.GetAllAsync();
		}

		// GET api/<GroupsController>/5
		[HttpGet("{id}")]
		public async Task<Group> GetAsync(int id)
		{
			return await _groupService.GetByIdAsync(id);
		}

		// POST api/<GroupsController>
		[HttpPost]
		public async Task Post([FromBody] Group value)
		{
			await _groupService.CreateAsync(value);
		}

		// PUT api/<GroupsController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<GroupsController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}

using System.API.Helpers;
using System.BLL.RoomManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace System.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomsController(IRoomService roomService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        // GET: api/<GroupsController>
        [HttpGet]
        public async Task<IEnumerable<Room>> Get() => await _roomService.GetAllAsync();

        // GET api/<GroupsController>/5
        [HttpGet("{id}")]
        public async Task<Room> GetAsync(int id) => await _roomService.GetAsync(id);

        // POST api/<GroupsController>
        [HttpPost]
        public async Task Post([FromBody] Room value) => await _roomService.CreateAsync(value);

        // PUT api/<GroupsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Room value) => await _roomService.CreateAsync(value);

        // DELETE api/<GroupsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id) => await _roomService.RemoveAsync(id);
    }
}
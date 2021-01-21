using System.API.Helpers;
using System.BLL.Models.RoomManagement;
using System.BLL.RoomManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        
        [HttpGet]
        public async Task<IEnumerable<Room>> Get()
        {
            try
            {

                var result = await _roomService.GetAllAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            
        }

        [HttpGet("{id}")]
        public async Task<Room> GetAsync(int id) => await _roomService.GetAsync(id);

        [HttpPost]
        public async Task<ActionResult<Room>> Post([FromBody] RoomCreate value)
        {
            try
            {

                var newRoom = await _roomService.CreateAsync(value);
                return StatusCode(StatusCodes.Status201Created, newRoom);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Room newRoom) => await _roomService.UpdateAsync(id, newRoom);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await _roomService.RemoveAsync(id);
    }
}
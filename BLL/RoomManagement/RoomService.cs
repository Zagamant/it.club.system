using System.BLL.Helpers;
using System.BLL.Models.RoomManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.RoomManagement
{
	public class RoomService : IRoomService
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public RoomService(DataContext context, IMapper mapper)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}


		public async Task<IEnumerable<RoomModel>> GetAllAsync()
		{
			var result =
				await _context.Rooms
					.Select(room => _mapper.Map<RoomModel>(room))
					.ToListAsync();
			
			return result;
		}
		
		public async Task<RoomModel> GetAsync(int roomId)
		{
			var result =
				await _context.Rooms.FirstOrDefaultAsync(room => room.Id == roomId);

			if (result == null) throw new AppException("Room not found");

			return _mapper.Map<RoomModel>(result);
		}

		public async Task<RoomModel> AddAsync(RoomCreateModel room)
		{
			var result = _mapper.Map<Room>(room);
			result.Club = await _context.Clubs.FirstAsync(club => club.Id == room.ClubId);
			await _context.AddAsync(result);
			await _context.SaveChangesAsync();
			return _mapper.Map<RoomModel>(result);
		}

		public async Task<RoomModel> UpdateAsync(int roomId, RoomModel newRoom)
		{
			var room = await _context.Rooms.FirstOrDefaultAsync(room => room.Id == roomId);
			if (room == null) throw new AppException("Room wasn't find");

			if (room.Club.Id != newRoom.ClubId)
			{
				var club = await _context.Clubs.FirstOrDefaultAsync(club => club.Id == newRoom.ClubId);
				if (club == null) throw new ArgumentException("club wasn't find");

				room.Club = club;
			}
			room.Capacity = newRoom.Capacity;
			room.RoomNumber = newRoom.RoomNumber;
			room.About = newRoom.About;
			room.Status = newRoom.Status;

			_context.Rooms.Update(room);

			await _context.SaveChangesAsync();

			return newRoom;
			
		}
		
		public async Task DeleteAsync(int roomId, bool isDelete = false)
		{
			var room = await _context.Rooms.FirstOrDefaultAsync(room => room.Id == roomId);
			if (room == null) throw new AppException("Room wasn't find");
			
			_context.Rooms.Remove(room);
			await _context.SaveChangesAsync();
		}
	}
}

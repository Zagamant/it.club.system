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


		public async Task<IEnumerable<Room>> GetAllAsync()
		{
			var result =
				await _context.Rooms.Where(room => room.Status != RoomStatus.Closed).ToListAsync();

			if (result == null) throw new AppException("Rooms not found");

			return result;
		}

		public async Task<Room> GetAsync(Room room)
		{
			var result =
				await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == room.RoomNumber && room.About == r.About);

			if (result == null) throw new AppException("Room not found");

			return result;
		}

		public async Task<Room> GetAsync(int roomId)
		{
			var result =
				await _context.Rooms.FirstOrDefaultAsync(room => room.Id == roomId);

			if (result == null) throw new AppException("Room not found");

			return result;
		}

		public async Task<Room> CreateAsync(RoomCreate room)
		{
			try
			{
				var roomTemp = _mapper.Map<Room>(room);
				roomTemp.Club = await _context.Clubs.FirstAsync(club => club.Id == room.ClubId);
				await _context.AddAsync(roomTemp);
				await _context.SaveChangesAsync();
				return roomTemp;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public async Task UpdateAsync(int roomId, Room newRoom)
		{
			var room = await _context.Rooms.FirstOrDefaultAsync(room => room.Id == roomId);
			if (room == null) throw new AppException("Room wasn't find");

			await UpdateAsync(room, newRoom);
		}

		public async Task UpdateAsync(Room room, Room newRoom)
		{
			newRoom.Id = room.Id;

			_context.Entry<Room>(room).State = EntityState.Detached;

			_context.Rooms.Update(newRoom);

			await _context.SaveChangesAsync();
		}

		public async Task RemoveAsync(int roomId)
		{
			var room = await _context.Rooms.FirstOrDefaultAsync(room => room.Id == roomId);
			if (room == null) throw new AppException("Room wasn't find");
			
			await RemoveAsync(room);
		}

		public async Task RemoveAsync(Room room)
		{
			_context.Rooms.Remove(room);
			await _context.SaveChangesAsync();
		}
		                            
	}
}

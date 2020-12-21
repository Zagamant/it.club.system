using System.BLL.Helpers;
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
		private DataContext _context;
		private IMapper _mapper;

		public RoomService(DataContext context, IMapper mapper)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}


		public async Task Create(Room room)
		{
			var roomTemp = _mapper.Map<Room>(room);
			await _context.AddAsync(roomTemp);
		}

		public async Task<Room> Get(Room room)
		{
			var result =
				await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == room.RoomNumber && room.About == r.About);

			if (result == null) throw new AppException("Room not found");

			return result;
		}

		public async Task<Room> Get(int roomId)
		{
			var result =
				await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

			if (result == null) throw new AppException("Room not found");

			return result;
		}

		public async Task<IEnumerable<Room>> GetAll()
		{
			var result =
				await _context.Rooms.Where(room => room.Status != RoomStatus.Closed).ToListAsync();

			if (result == null) throw new AppException("Rooms not found");

			return result;
		}

		public async Task Update(int roomId, Room newRoom)
		{
			var roomTemp = _mapper.Map<Room>(newRoom);
			roomTemp.Id = roomId;

			_context.Rooms.Update(roomTemp);

			await _context.SaveChangesAsync();
		}

		public async Task Update(Room room, Room newRoom)
		{
			await Update(room.Id, newRoom);
		}

		public async Task Remove(int roomId)
		{
			var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
			if (room == null) throw new AppException("Room wasn't find");
			_context.Rooms.Remove(room);
			await _context.SaveChangesAsync();
		}

		public async Task Remove(Room room)
		{
			await Remove(room.Id);
		}

		#region PrivateHelpers

		private async Task<Room> GetRoomBySafeModel(RoomSafeModel roomSm)
		{
			var result =
				await _context.Rooms.FirstOrDefaultAsync(r =>
					roomSm.ClubTitle == r.Club.Title && roomSm.RoomNumber == r.RoomNumber);

			if (result == null) throw new AppException("Room wasn't find");

			return result;
		}

		#endregion
	}
}

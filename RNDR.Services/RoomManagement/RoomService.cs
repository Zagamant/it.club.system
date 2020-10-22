using System.BLL.Helpers;
using System.BLL.Models.RoomManagement;
using System.DAL;
using System.DAL.Models;
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


		public async Task Create(RoomModel room)
		{
			var roomTemp = _mapper.Map<Room>(room);
			await _context.AddAsync(roomTemp);
		}

		public async Task<Room> GetRoom(RoomSafeModel room)
		{
			var result =
				await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == room.RoomNumber && room.About == r.About);

			if (result == null) throw new AppException("Room not found");

			return result;
		}

		public async Task Update(int roomId, RoomModel newRoom)
		{
			var roomTemp = _mapper.Map<Room>(newRoom);
			roomTemp.Id = roomId;

			_context.Rooms.Update(roomTemp);

			await _context.SaveChangesAsync();
		}

		public async Task Update(RoomSafeModel room, RoomModel newRoom)
		{
			var result = GetRoomBySafeModel(room);
			await Update(result.Id, newRoom);
		}

		public async Task Remove(int roomId)
		{
			var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
			if (room == null) throw new AppException("Room wasn't find");
			_context.Rooms.Remove(room);
			await _context.SaveChangesAsync();
		}

		public async Task Remove(RoomSafeModel room)
		{
			var result = GetRoomBySafeModel(room);
			await Remove(result.Id);
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

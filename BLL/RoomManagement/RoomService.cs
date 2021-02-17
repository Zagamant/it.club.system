using System.BLL.Helpers;
using System.BLL.Models.RoomManagement;
using System.DAL;
using System.DAL.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.RoomManagement
{
    public class RoomService : Repository<int, Room, RoomCreateModel, RoomModel, RoomModel>, IRoomService
    {
        public RoomService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _table = _context.Rooms;
        }

        public override async Task<RoomModel> AddAsync(RoomCreateModel room)
        {
            var result = _mapper.Map<Room>(room);
            result.Club = await _context.Clubs.FirstAsync(club => club.Id == room.ClubId);
            await _context.AddAsync(result);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoomModel>(result);
        }

        public override async Task<RoomModel> UpdateAsync(int roomId, RoomModel newRoom)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(roomIter => roomIter.Id == roomId);
            if (room == null) throw new AppException("Room wasn't find");

            if (room.Club.Id != newRoom.ClubId)
            {
                var club = await _context.Clubs.FirstOrDefaultAsync(clubIter => clubIter.Id == newRoom.ClubId);
                if (club == null) throw new ArgumentException("club wasn't find");

                room.Club = club;
            }

            room.Capacity = newRoom.Capacity;
            room.Number = newRoom.Number;
            room.About = newRoom.About;
            room.Status = newRoom.Status;

            _context.Rooms.Update(room);

            await _context.SaveChangesAsync();

            return newRoom;
        }
    }
}
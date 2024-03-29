﻿using System.BLL.Helpers;
using System.BLL.Models.RoomManagement;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace System.BLL.RoomManagement
{
    public class RoomService : BaseService<int, Room, RoomCreateModel, RoomModel, RoomModel>, IRoomService
    {
        public RoomService(DataContext context, IMapper mapper, ILogger<RoomService> logger) : base(context, mapper, logger)
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

        public override async Task<RoomModel> UpdateAsync(int roomId, RoomModel updated)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(roomIter => roomIter.Id == roomId);
            if (room == null) throw new AppException("Room wasn't find");

            if (room.Club.Id != updated.ClubId)
            {
                var club = await _context.Clubs.FirstOrDefaultAsync(clubIter => clubIter.Id == updated.ClubId);
                if (club == null) throw new ArgumentException("club wasn't find");

                room.Club = club;
            }
            
            room.Capacity = updated.Capacity;
            room.Number = updated.Number;
            room.About = updated.About;
            room.Status = updated.Status;
            
            
            var idsToAdd = updated.GroupIds.Except(room.Groups.Select(u => u.Id));
            
            var IdsToRemove = room.Groups.Select(u => u.Id).Except(updated.GroupIds);
            
            foreach (var oldGroupUser in room.Groups.Where(user => IdsToRemove.Contains(user.Id)))
            {
                room.Groups.Remove(oldGroupUser);
            }

            foreach (var oldGroupUser in _context.Groups.Where(user => idsToAdd.Contains(user.Id)))
            {
                room.Groups.Add(oldGroupUser);
            }

            _context.Rooms.Update(room);

            await _context.SaveChangesAsync();

            return updated;
        }
    }
}
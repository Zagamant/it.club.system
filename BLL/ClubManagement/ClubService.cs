﻿using System.BLL.Helpers;
using System.BLL.Models.ClubManagement;
using System.BLL.Models.UserManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace System.BLL.ClubManagement
{
    public class ClubService : BaseService<int, Club, ClubModel,ClubModel,ClubModel>, IClubService
    {
        private readonly RoleManager<Role> _roleManager;

        public ClubService(
            DataContext context,
            IMapper mapper,
            RoleManager<Role> roleManager, ILogger<ClubService> logger) : base(context, mapper, logger)
        {
            _roleManager = roleManager;
            _table = _context.Clubs;
        }

        public override async Task<ClubModel> AddAsync(ClubModel club)
        {
            if (_context.Clubs.Any(x => x.Title == club.Title))
                throw new AppException("Club name: \"" + club.Title + "\" is already taken");

            var newClub = _mapper.Map<Club>(club);
            
            newClub.Address ??= new Address
            {
                City = club.City,
                AddressLine = club.AddressLine,
                Country = club.Country
            };

            var role = new Role(newClub.Title);
            await _roleManager.CreateAsync(role);
            newClub.Permissions.Add(role);
            await _context.Clubs.AddAsync(newClub);

            await _context.SaveChangesAsync();

            return _mapper.Map<ClubModel>(newClub);
        }
        
        public  async Task<ClubModel> AddRoomAsync(int clubId, int roomId, string userId)
        {
            var club = await GetByIdFullClubAsync(clubId);
            
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null) throw new AppException("Room not found");

            club.Rooms.Add(room);
            _context.Clubs.Update(club);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClubModel>(club);
        }

        public async Task<ClubModel> RemoveRoomAsync(int clubId, int roomId, string userId, bool isDeleteRoom = false)
        {
            var club = await GetByIdFullClubAsync(clubId);
            
            var room = await _context.Rooms.FirstOrDefaultAsync(roomItem =>
                roomItem.Id == roomId && roomItem.Club.Id == club.Id);

            if (room == null) throw new ArgumentNullException(nameof(room));

            if (!club.Rooms.Contains(room))
                throw new AppException("Room: " + room.Number + " in club " + club.Title + " not found.");

            if (isDeleteRoom)
            {
                _context.Rooms.Remove(room);
            }
            else
            {
                room.Status = RoomStatus.Closed;
                _context.Rooms.Update(room);
            }

            club.Rooms.Remove(room);

            _context.Clubs.Update(club);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClubModel>(club);
        }

        public async Task<IEnumerable<ClubModel>> GetByUserId(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            
            //TODO: HERNYA
            // var clubs = _context.Groups
            //     .Where(c => c.Users.Contains(user))
            //     .Select(g => _context.Rooms.Where(c => c.Groups.Contains(g)))
            //     .Select(ar => ar.Select(el => el.Club))
            //     .SelectMany(item => item)
            //     .Distinct();

            var clubs = _context.Groups
                .Where(gr => gr.Users.Contains(user))
                .Select(gr => gr.Room)
                .Select(r => r.Club)
                .Distinct();
            
            return clubs.Select(c => _mapper.Map<ClubModel>(c)).ToList();

        }

        public override async Task<ClubModel> UpdateAsync(int clubId, ClubModel updated)
        {
            var club = await GetByIdFullClubAsync(clubId);
            updated.Id = club.Id;

            if (updated.Title != club.Title)
            {
                var clubRole = await _context.Roles.FirstAsync(role => role.Name == club.Title);
                clubRole.Name = updated.Title;
                club.Title = updated.Title;
                _context.Roles.Update(clubRole);
            }

            club.Address ??= new Address
            {
                AddressLine = updated.AddressLine,
                City = updated.City,
                Country = updated.Country
            };

            if (!string.IsNullOrEmpty(updated.Country) && club.Address.Country != updated.Country)
            {
                club.Address.Country = updated.Country;
            }

            if (!string.IsNullOrEmpty(updated.City) && club.Address.City != updated.City)
            {
                club.Address.City = updated.City;
            }

            if (!string.IsNullOrEmpty(updated.AddressLine) && club.Address.AddressLine != updated.AddressLine)
            {
                club.Address.AddressLine = updated.AddressLine;
            }
            
            _context.Clubs.Update(club);
            
            await _context.SaveChangesAsync();

            return _mapper.Map<ClubModel>(club);
        }

        public override async Task DeleteAsync(int id, bool isDelete = false)
        {
            var entity = await _table.FindAsync(id);
            if (entity == null)
                throw new ArgumentException($"Club with id: {id} not found");

            var clubRole = await _context.Roles.FirstAsync(r => r.Name == entity.Title);
            
            _context.Roles.Remove(clubRole);
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
        #region PrivateHelpers

        private async Task<Club> GetByIdFullClubAsync(int clubId)
        {
            //if (user == null) throw new AppException("User not exist");

            //var roles = await _userManager.GetRolesAsync(user);
            //var user = await _userManager.FindByIdAsync(userId);
            //var roles = await _userManager.GetRolesAsync(user);

            var club = await _context.Clubs
                //.Where(club => club.Permissions.Select(role => role.Name).Intersect(roles).Any())
                .FirstOrDefaultAsync(cl => cl.Id == clubId);

            if (club == null) throw new AppException("Club not found");

            return club;
        }

        #endregion
    }
}
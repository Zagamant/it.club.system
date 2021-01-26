﻿using System.BLL.Helpers;
using System.BLL.Models.ClubManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.ClubManagement
{
    public class ClubService : IClubService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public ClubService(
            DataContext dataContext,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IEnumerable<Club>> GetAllAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            // var roles = await _userManager.GetRolesAsync(user);

            var allClubs = await _context.Clubs
                //.AsNoTracking() //WTF cause internal server error one i ca not track for 3 DAY AF //TODO stackoverflow quest;
                //.Where(club => club.Permissions.Any(role => roles.Contains(role.Name)))
                .ToListAsync();

            return allClubs;
        }

        public async Task<Club> GetByIdAsync(int clubId, string userId)
        {
            var club = await GetByIdFullClubAsync(clubId, userId);

            if (club == null) throw new AppException("Club not found");

            return club;
        }

        public async Task<Club> GetByTitleAsync(string clubTitle, string userId)
        {
            var club = await GetByTitleFullClubAsync(clubTitle, userId);

            if (club == null) throw new AppException("Club not found");

            var resultClub = _mapper.Map<Club>(club);

            return resultClub;
        }

        public async Task<Club> CreateAsync(Club club)
        {
            if (club == null) throw new ArgumentNullException(nameof(club));

            if (_context.Clubs.Any(x => x.Title == club.Title))
                throw new AppException("Club name: \"" + club.Title + "\" is already taken");


            var role = new Role(club.Title);
            await _roleManager.CreateAsync(role);
            club.Permissions.Add(role);
            await _context.Clubs.AddAsync(club);

            await _context.SaveChangesAsync();

            return club;
        }

        public async Task AddRoomAsync(int clubId, Room room, string userId)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));

            var club = await GetByIdFullClubAsync(clubId, userId);
            var roomTemp =
                await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == room.RoomNumber && r.Club == room.Club);

            if (roomTemp == null) throw new AppException("Room not found");

            club.Rooms.Add(roomTemp);

            _context.Clubs.Update(club);

            await _context.SaveChangesAsync();
        }

        public async Task AddRoomAsync(Club club, Room room, string userId)
        {
            if (club == null) throw new ArgumentNullException(nameof(club));
            if (room == null) throw new ArgumentNullException(nameof(room));

            club.Rooms.Add(room);
            _context.Clubs.Update(club);
            await _context.SaveChangesAsync();
        }

        public async Task AddRoomAsync(int clubId, int roomId, string userId)
        {
            var club = await GetByIdFullClubAsync(clubId, userId);
            var room =
                await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

            if (room == null) throw new AppException("Room not found");

            await AddRoomAsync(club, room, userId);
        }

        public async Task AddRoomAsync(Club club, int roomId, string userId)
        {
            var roomTemp =
                await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

            await AddRoomAsync(club, roomTemp, userId);
        }


        public async Task RemoveRoomAsync(int clubId, Room room, string userId, bool isDeleteRoom = false)
        {
            var club = await GetByIdFullClubAsync(clubId, userId);

            await RemoveRoomAsync(club, room, userId);
        }

        public async Task RemoveRoomAsync(Club club, Room room, string userId, bool isDeleteRoom = false)
        {
            if (club == null) throw new ArgumentNullException(nameof(club));
            if (room == null) throw new ArgumentNullException(nameof(room));

            if (!club.Rooms.Contains(room))
                throw new AppException("Room: " + room.RoomNumber + " in club " + club.Title + " not found.");

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
        }

        public async Task RemoveRoomAsync(int clubId, int roomId, string userId, bool isDeleteRoom = false)
        {
            var club = await GetByIdFullClubAsync(clubId, userId);
            var room = await _context.Rooms.FirstOrDefaultAsync(roomItem =>
                roomItem.Id == roomId && roomItem.Club.Id == club.Id);

            await RemoveRoomAsync(club, room, userId);
        }

        public async Task<Club> UpdateAsync(int clubId, ClubModel newClub, string userId)
        {
            var club = await GetByIdFullClubAsync(clubId, userId);

            return await UpdateAsync(club, newClub, userId);
        }

        public async Task<Club> UpdateAsync(Club club, ClubModel newClub, string userId)
        {
            try
            {
                newClub.Id = club.Id;

                if (newClub.Title != club.Title)
                {
                    club.Title = newClub.Title;

                    var clubRole = await _context.Roles.FirstAsync(role => role.Name == newClub.Title);
                    clubRole.Name = newClub.Title;

                    _context.Roles.Update(clubRole);
                }

                if (club.Address == null)
                {
                    var address = new Address
                    {
                        AddressLine = newClub.AddressLine,
                        City = newClub.City,
                        Country = newClub.Country
                    };
                    await _context.Addresses.AddAsync(address);
                    club.Address = address;
                }

                if (!string.IsNullOrEmpty(newClub.Country) && club.Address.Country != newClub.Country)
                {
                    club.Address.Country = newClub.Country;
                }

                if (!string.IsNullOrEmpty(newClub.City) && club.Address.City != newClub.City)
                {
                    club.Address.City = newClub.City;
                }

                if (!string.IsNullOrEmpty(newClub.AddressLine) && club.Address.AddressLine != newClub.AddressLine)
                {
                    club.Address.AddressLine = newClub.AddressLine;
                }


                _context.Clubs.Update(club);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return club;
        }

        public async Task RemoveAsync(int clubId, string userId, bool isDelete = false)
        {
            var club = await GetByIdFullClubAsync(clubId, userId);

            if (!_context.Clubs.Contains(club)) throw new AppException("Club: " + club.Title + " not found.");

            if (isDelete)
            {
                _context.Clubs.Remove(club);
            }
            else
            {
                club.Status = ClubStatus.Closed;
                _context.Clubs.Update(club);
            }

            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(Club club, string userId, bool isDelete = false)
        {
            await RemoveAsync(club.Id, userId);
        }

        #region PrivateHelpers

        private async Task<Club> GetByIdFullClubAsync(int clubId, string userId)
        {
            //if (user == null) throw new AppException("User not exist");

            //var roles = await _userManager.GetRolesAsync(user);
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            var resultClub = await _context.Clubs
                //.Where(club => club.Permissions.Select(role => role.Name).Intersect(roles).Any())
                .FirstOrDefaultAsync(cl => cl.Id == clubId);

            if (resultClub == null) throw new AppException("Club not found");

            return resultClub;
        }

        private async Task<Club> GetByTitleFullClubAsync(string title, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            var resultClub = await _context.Clubs
                .Where(club => club.Permissions.Select(role => role.Name).Intersect(roles).Any())
                .FirstOrDefaultAsync(cl => cl.Title == title);

            if (resultClub == null) throw new AppException("Club not found");

            return resultClub;
        }

        #endregion
    }
}
using System.BLL.Helpers;
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


        public async Task<IEnumerable<ClubModel>> GetAllAsync()
        {
            //var user = await _userManager.FindByIdAsync(userId);

            // var roles = await _userManager.GetRolesAsync(user);

            var allClubs = await _context.Clubs
                //.AsNoTracking() //WTF cause internal server error one i ca not track for 3 DAY AF //TODO stackoverflow quest;
                //.Where(club => club.Permissions.Any(role => roles.Contains(role.Name)))
                .Select(club => _mapper.Map<ClubModel>(club))
                .ToListAsync();

            return allClubs;
        }

        public async Task<ClubModel> GetAsync(int clubId)
        {
            var club = await GetByIdFullClubAsync(clubId);

            return _mapper.Map<ClubModel>(club);
        }

        public async Task<ClubModel> GetByTitleAsync(string clubTitle, string userId)
        {
            var club = await _context.Clubs.FirstOrDefaultAsync(clb => clb.Title == clubTitle);
            if (club == null) throw new ArgumentNullException(nameof(club));

            return _mapper.Map<ClubModel>(club);
        }

        public async Task<ClubModel> AddAsync(ClubModel club)
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

        public async Task<ClubModel> AddRoomAsync(int clubId, int roomId, string userId)
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

            return _mapper.Map<ClubModel>(club);
        }

        public async Task<ClubModel> UpdateAsync(int clubId, ClubModel newClub)
        {
            var club = await GetByIdFullClubAsync(clubId);
            newClub.Id = club.Id;

            if (newClub.Title != club.Title)
            {
                club.Title = newClub.Title;

                var clubRole = await _context.Roles.FirstAsync(role => role.Name == newClub.Title);
                clubRole.Name = newClub.Title;

                _context.Roles.Update(clubRole);
            }

            club.Address ??= new Address
            {
                AddressLine = newClub.AddressLine,
                City = newClub.City,
                Country = newClub.Country
            };

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

            return _mapper.Map<ClubModel>(club);
        }
        
        public async Task DeleteAsync(int clubId, bool isDelete = false)
        {
            var club = await GetByIdFullClubAsync(clubId);

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
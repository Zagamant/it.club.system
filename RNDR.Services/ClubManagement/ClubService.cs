using System.BLL.Helpers;
using System.BLL.Models.ClubManagement;
using System.BLL.Models.RoomManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using System.Linq;
using System.Security.Claims;
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
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

		public ClubService(
			DataContext dataContext, 
			IMapper mapper, 
			UserManager<User> userManager, 
			RoleManager<Role> roleManager)
		{
			_context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			this._userManager = userManager;
			_roleManager = roleManager;
		}


		public async Task<IEnumerable<ClubModel>> GetAllAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var roles = await _userManager.GetRolesAsync(user);

			var allClubs = await _context.Clubs
				.AsNoTracking()
				.Where(club => club.Permissions.Select(role => role.Name).Intersect(roles).Any())
				.Select(c => new ClubModel
				{
					Address = c.Address,
					Rooms = c.Rooms,
					Status = c.Status,
					Title = c.Title
				}).ToListAsync();
			return allClubs;
		}

		public async Task<ClubModel> GetByIdAsync(int clubId, string userId)
		{
			
			var club = await GetByIdFullClubAsync(clubId, userId);

			if (club == null) throw new AppException("Club not found");

			var resultClub = _mapper.Map<ClubModel>(club);

			return resultClub;
		}

		public async Task<ClubModel> GetByTitleAsync(string clubTitle, string userId)
		{
			var club = await GetByTitleFullClubAsync(clubTitle, userId);

			if (club == null) throw new AppException("Club not found");

			var resultClub = _mapper.Map<ClubModel>(club);

			return resultClub;
		}

		public async Task<ClubModel> CreateAsync(ClubRegister club)
		{
			if (club == null) throw new ArgumentNullException(nameof(club));
			
			if (_context.Clubs.Any(x => x.Title == club.Title))
				throw new AppException("Club name: \"" + club.Title + "\" is already taken");

			var clubDal = _mapper.Map<Club>(club);

			var role = new Role(clubDal.Title);
			await _roleManager.CreateAsync(role);
			clubDal.Permissions.Add(role);

			await _context.Clubs.AddAsync(clubDal);

			var resultClub = _mapper.Map<ClubModel>(clubDal);

			await _context.SaveChangesAsync();

			return resultClub;
		}

		public async Task AddRoomAsync(int clubId, RoomModel room,string userId)
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

		public async Task AddRoomAsync(ClubSafeModel club, RoomModel room, string userId)
		{
			if (club == null) throw new ArgumentNullException(nameof(club));
			if (room == null) throw new ArgumentNullException(nameof(room));

			var clubTemp = await GetByTitleFullClubAsync(club.Title, userId);
			var roomTemp =
				await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == room.RoomNumber && r.Club == room.Club);

			if (clubTemp == null) throw new AppException("Club not found");
			if (roomTemp == null) throw new AppException("Room not found");


			clubTemp.Rooms.Add(roomTemp);
			_context.Clubs.Update(clubTemp);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveRoomAsync(int clubId, RoomModel room,string userId, bool isDeleteRoom = false)
		{
			var club = await GetByIdFullClubAsync(clubId, userId);

			var roomTemp =
				await _context.Rooms.FirstOrDefaultAsync(r => r.Club == room.Club && r.RoomNumber == room.RoomNumber);

			if (roomTemp == null)
			{
				throw new AppException("Room: " + room.RoomNumber + " in club " + club.Title + " not found.");
			}

			if (isDeleteRoom)
			{
				_context.Rooms.Remove(roomTemp);
			}
			else
			{
				room.Status = RoomStatus.Closed;
				_context.Rooms.Update(roomTemp);
			}

			club.Rooms.Remove(roomTemp);

			_context.Clubs.Update(club);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveRoomAsync(ClubSafeModel club, RoomModel room, string userId, bool isDeleteRoom = false)
		{
			var resultClub = await GetByTitleFullClubAsync(club.Title, userId);

			var roomTemp =
				await _context.Rooms.FirstOrDefaultAsync(r => r.Club == room.Club && r.RoomNumber == room.RoomNumber);

			if (roomTemp == null)
			{
				throw new AppException("Room: " + room.RoomNumber + " in club " + resultClub.Title + " not found.");
			}

			if (isDeleteRoom)
			{
				_context.Rooms.Remove(roomTemp);
			}
			else
			{
				room.Status = RoomStatus.Closed;
				_context.Rooms.Update(roomTemp);
			}

			resultClub.Rooms.Remove(roomTemp);

			_context.Clubs.Update(resultClub);
			await _context.SaveChangesAsync();
		}

		public async Task<ClubModel> UpdateAsync(int clubId, ClubSafeModel newClub, string userId)
		{
			var club = await GetByIdFullClubAsync(clubId, userId);

			_context.Clubs.Update(club);
			await _context.SaveChangesAsync();

			var resultClub = _mapper.Map<ClubModel>(club);

			return resultClub;
		}

		public async Task<ClubModel> UpdateAsync(Club club, ClubSafeModel newClub, string userId)
		{
			return await UpdateAsync(club.Id, newClub, userId);
		}

		public async Task RemoveAsync(int clubId, string userId, bool isDelete = false)
		{
			var club = await GetByIdFullClubAsync(clubId, userId);

			if (!_context.Clubs.Contains(club))
			{
				throw new AppException("Club: " + club.Title + " not found.");
			}

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


		public async Task RemoveAsync(ClubSafeModel club, string userId, bool isDelete = false)
		{
			var clubT = await GetByTitleFullClubAsync(club.Title, userId);

			if (isDelete)
			{
				_context.Clubs.Remove(clubT);
			}
			else
			{
				clubT.Status = ClubStatus.Closed;
				_context.Clubs.Update(clubT);
			}

			await _context.SaveChangesAsync();
		}

		#region PrivateHelpers

		private async Task<Club> GetByIdFullClubAsync(int clubId, string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var roles = await _userManager.GetRolesAsync(user);

			var resultClub = await _context.Clubs
				.Where(club => club.Permissions.Select(role => role.Name).Intersect(roles).Any())
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
using System.BLL.Helpers;
using System.BLL.Models.ClubManagement;
using System.BLL.Models.RoomManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.DAL.Entities.Enums;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.ClubManagement
{
	public class ClubService : IClubService
	{
		private readonly DataContext _context;
		private IMapper _mapper;

		public ClubService(DataContext dataContext, IMapper mapper)
		{
			_context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}


		public async Task<IEnumerable<ClubModel>> GetAll()
		{
			var allClubs = await _context.Clubs.Select(c => new ClubModel
			{
				Address = c.Address,
				Rooms = c.Rooms,
				Status = c.Status,
				Title = c.Title
			}).ToListAsync();
			return allClubs;
		}

		public async Task<ClubModel> GetById(int clubId)
		{
			var club = await GetByIdFullClubAsync(clubId);

			if (club == null) throw new AppException("Club not found");

			var resultClub = _mapper.Map<ClubModel>(club);

			return resultClub;
		}

		public async Task<ClubModel> GetByTitle(string clubTitle)
		{
			var club = await GetByTitleFullClubAsync(clubTitle);

			if (club == null) throw new AppException("Club not found");

			var resultClub = _mapper.Map<ClubModel>(club);

			return resultClub;
		}

		public async Task<ClubModel> CreateClub(ClubRegister club)
		{
			if (club == null) throw new ArgumentNullException(nameof(club));

			if (_context.Clubs.Any(x => x.Title == club.Title)) throw new AppException("Username \"" + club.Title + "\" is already taken");

			var clubDal = _mapper.Map<Club>(club);

			await _context.Clubs.AddAsync(clubDal);

			var resultClub = _mapper.Map<ClubModel>(clubDal);

			return resultClub;
		}

		public async Task AddRoom(int clubId, RoomModel room)
		{
			if (room == null) throw new ArgumentNullException(nameof(room));

			var club = await GetByIdFullClubAsync(clubId);
			var roomTemp =
				await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == room.RoomNumber && r.Club == room.Club);
			
			if (roomTemp == null) throw new AppException("Room not found");

			club.Rooms.Add(roomTemp);

			_context.Clubs.Update(club);

			await _context.SaveChangesAsync();
		}

		public async Task AddRoom(ClubSafeModel club, RoomModel room)
		{
			if (club == null) throw new ArgumentNullException(nameof(club));
			if (room == null) throw new ArgumentNullException(nameof(room));

			var clubTemp = await GetByTitleFullClubAsync(club.Title);
			var roomTemp =
				await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == room.RoomNumber && r.Club == room.Club);

			if (clubTemp == null) throw new AppException("Club not found");
			if (roomTemp == null) throw new AppException("Room not found");
			

			clubTemp.Rooms.Add(roomTemp);
			_context.Clubs.Update(clubTemp);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveRoom(int clubId, RoomModel room, bool isDeleteRoom = false)
		{
			var club = await GetByIdFullClubAsync(clubId);

			var roomTemp = await _context.Rooms.FirstOrDefaultAsync(r => r.Club == room.Club && r.RoomNumber == room.RoomNumber);

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

		public async Task RemoveRoom(ClubSafeModel club, RoomModel room, bool isDeleteRoom = false)
		{
			var resultClub = await GetByTitleFullClubAsync(club.Title);

			var roomTemp = await _context.Rooms.FirstOrDefaultAsync(r => r.Club == room.Club && r.RoomNumber == room.RoomNumber);

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

		public async Task<ClubModel> UpdateClub(int clubId, ClubSafeModel newClub)
		{
			var club = await GetByIdFullClubAsync(clubId);

			_context.Clubs.Update(club);
			await _context.SaveChangesAsync();

			var resultClub = _mapper.Map<ClubModel>(club);

			return resultClub;
		}

		public async Task<ClubModel> UpdateClub(Club club, ClubSafeModel newClub)
		{
			return await UpdateClub(club.Id, newClub);
		}

		public async Task RemoveClub(int clubId, bool isDelete = false)
		{
			var club = await GetByIdFullClubAsync(clubId);

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


		public async Task RemoveClub(ClubSafeModel club, bool isDelete = false)
		{
			var clubT = await GetByTitleFullClubAsync(club.Title);

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
		private async Task<Club> GetByIdFullClubAsync(int clubId)
		{
			var club = await _context.Clubs.FirstOrDefaultAsync(cl => cl.Id == clubId);

			if (club == null) throw new AppException("Club not found");

			return club;
		}

		private async Task<Club> GetByTitleFullClubAsync(string title)
		{
			var club = await _context.Clubs.FirstOrDefaultAsync(cl => cl.Title == title);

			if (club == null) throw new AppException("Club not found");

			return club;
		}

		#endregion

	}
}

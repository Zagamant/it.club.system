using System;
using System.BLL.Helpers;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.PhotoManagement
{
	public class PhotoService : IPhotoService
	{
		private readonly DataContext _context;

		public PhotoService(DataContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<Photo> GetAsync(int id)
		{
			return await _context.Photos
				.AsNoTracking()
				.SingleOrDefaultAsync(photo => photo.Id == id);
		}

		public async Task<IEnumerable<Photo>> GetAllAsync()
		{
			return await _context.Photos
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<Photo>> GetAllAsync(int userId)
		{
			return await _context.Photos
				.AsNoTracking()
				.Where(photo => photo.UserId == userId)
				.ToListAsync();
		}

		public async Task<IEnumerable<Photo>> GetAllAsync(User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));

			return await _context.Photos
				.AsNoTracking()
				.Where(photo => photo.User == user)
				.ToListAsync();
		}

		public async Task AddAsync(User user, Photo photo)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));
			if (photo == null) throw new ArgumentNullException(nameof(photo));

			photo.User = user;
			await _context.Photos.AddAsync(photo);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Photo oldPhoto, Photo newPhoto)
		{
			if (oldPhoto == null) throw new ArgumentNullException(nameof(oldPhoto));
			if (newPhoto == null) throw new ArgumentNullException(nameof(newPhoto));

			oldPhoto.PhotoAsBytes = newPhoto.PhotoAsBytes;
			_context.Photos.Update(oldPhoto);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(int id, Photo newPhoto)
		{
			if (newPhoto == null) throw new ArgumentNullException(nameof(newPhoto));

			newPhoto.Id = id;
			_context.Photos.Update(newPhoto);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Photo photo)
		{
			if (photo == null) throw new ArgumentNullException(nameof(photo));

			_context.Photos.Remove(photo);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var photo = await _context.Photos.SingleOrDefaultAsync(ph => ph.Id == id);

			if (photo == null) throw new AppException($"Cant find photo with id: {id}");

			_context.Photos.Remove(photo);
			await _context.SaveChangesAsync();
		}
	}
}
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.PhotoManagement
{
	public interface IPhotoService
	{
		Task<Photo> GetAsync(int id);
		Task<IEnumerable<Photo>> GetAllAsync();
		Task<IEnumerable<Photo>> GetAllAsync(int userId);
		Task<IEnumerable<Photo>> GetAllAsync(User user);
		Task AddAsync(User user, Photo photo);
		Task UpdateAsync(Photo oldPhoto, Photo newPhoto);
		Task UpdateAsync(int id, Photo newPhoto);
		Task DeleteAsync(Photo photo);
		Task DeleteAsync(int id);
	}
}

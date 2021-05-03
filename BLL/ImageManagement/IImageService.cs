using System.BLL.Models.PhotoManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.PhotoManagement
{
    public interface IImageService
    {
        Task<IEnumerable<ImageModel>> GetAsync();
        Task<ImageModel> GetByIdAsync(int id);
        Task<IEnumerable<ImageModel>> GetByUserAsync(int userId);
        Task<ImageModel> Upload(UploadImageModel image);
        Task Delete(int id);
    }
}
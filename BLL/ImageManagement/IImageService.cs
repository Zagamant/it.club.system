using System.BLL.Models.ImageManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.ImageManagement
{
    public interface IImageService
    {
        Task<IEnumerable<ImageModel>> GetAsync();
        Task<ImageModel> GetByIdAsync(int id);
        Task<IEnumerable<ImageModel>> GetByUserAsync(int userId);
        Task<ImageModel> UploadAsync(UploadImageModel image);
        Task Delete(int id);
    }
}
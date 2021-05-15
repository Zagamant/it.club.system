using System.BLL.Models.ImageManagement;
using System.Net.Http;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.PhotoManagement
{
    public class PhotoService : Repository<int, ImageModel, ImageModel, ImageModel>,
        IPhotoService
    {
        public PhotoService(HttpClient http) : base(http)
        {
        }
        
    }
}
using System.BLL.Models.ImageManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.PhotoManagement
{
    public interface IPhotoService : IRepository<int, ImageModel, ImageModel, ImageModel>
    {
    }
}
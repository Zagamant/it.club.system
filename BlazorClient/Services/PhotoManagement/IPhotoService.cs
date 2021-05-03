using System.BLL.Models.PhotoManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.PhotoManagement
{
    public interface IPhotoService : IRepository<int, ImageModel, ImageModel, ImageModel>
    {
    }
}
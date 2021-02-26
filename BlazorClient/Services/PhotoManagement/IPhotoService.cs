using System.BLL.Models.PhotoManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.PhotoManagement
{
    public interface IPhotoService : IRepository<int, PhotoModel, PhotoModel, PhotoModel>
    {
    }
}
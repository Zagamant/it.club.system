using System.BLL.Models.PhotoManagement;
using BlazorClient.Services.Helpers;
using System.Drawing;
using System.Drawing.Imaging;

namespace BlazorClient.Services.PhotoManagement
{
    public interface IPhotoService : IRepository<int, PhotoModel, PhotoModel, PhotoModel>
    {
        

    }
}
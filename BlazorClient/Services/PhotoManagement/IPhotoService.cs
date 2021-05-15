using System.BLL.Models.ImageManagement;
using BlazorClient.Services.Helpers;
using System.Drawing;
using System.Drawing.Imaging;

namespace BlazorClient.Services.PhotoManagement
{
    public interface IPhotoService : IRepository<int, ImageModel, ImageModel, ImageModel>
    {
        

    }
}
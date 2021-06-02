using System.BLL.Models.ImageManagement;
using BlazorClient.Services.Helpers;
using System.Drawing;
using System.Drawing.Imaging;

namespace BlazorClient.Services.PhotoManagement
{
    public interface IPhotoService : IServiceBase<int, ImageModel, ImageModel, ImageModel>
    {
        

    }
}
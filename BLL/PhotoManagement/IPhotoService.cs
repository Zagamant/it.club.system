using System.BLL.Helpers;
using System.BLL.Models.PhotoManagement;

namespace System.BLL.PhotoManagement
{
    public interface IPhotoService : IRepository<int, PhotoModel, PhotoModel, PhotoModel>
    {
    }
}
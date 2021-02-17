using System.API.Helpers;
using System.BLL.Models.PhotoManagement;
using System.BLL.PhotoManagement;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PhotosController : BaseController<IPhotoService, PhotoModel,PhotoModel,PhotoModel>
    {
        public PhotosController(IPhotoService service) : base(service)
        {
            
        }
    }
}
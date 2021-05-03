using System.BLL.PhotoManagement;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController
    {
        public ImageController(IImageService service)
        {
        }
    }
}
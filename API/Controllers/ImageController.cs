using System.BLL.ImageManagement;
using System.BLL.Models.ImageManagement;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly IImageService _service;

        public ImageController(IImageService service, IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv ?? throw new ArgumentNullException(nameof(hostingEnv));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] UploadImageModel imageModel)
        {
            // if (imageModel.Image == null) 
            //     return BadRequest();
            //
            // var a = _hostingEnv.WebRootPath;
            // var fileName = Path.GetFileName(imageModel.Image.FileName);
            // var filePath = Path.Combine(_hostingEnv.WebRootPath, "images\\Cars", fileName);
            //
            // await using (var fileSteam = new FileStream(filePath, FileMode.Create))
            // {
            //     await imageModel.Image.CopyToAsync(fileSteam);
            // }
            //
            // ImageModel image = new()
            // {
            //     ImagePath = filePath
            // };
            //save the filePath to database ImagePath field.
            await _service.UploadAsync(imageModel);
            return Ok();

        }
    }
}
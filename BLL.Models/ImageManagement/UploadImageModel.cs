using Microsoft.AspNetCore.Http;

namespace System.BLL.Models.ImageManagement
{
    public class UploadImageModel
    {
        public IFormFile Image { get; set; }

        public int UserId { get; set; }

    }
}
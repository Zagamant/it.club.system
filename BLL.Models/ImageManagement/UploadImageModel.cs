using Microsoft.AspNetCore.Http;

namespace System.BLL.Models.PhotoManagement
{
    public class UploadImageModel
    {
        public IFormFile Image { get; set; }

        public int UserId { get; set; }

    }
}
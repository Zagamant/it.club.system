using System.BLL.Models.Helpers;

namespace System.BLL.Models.ImageManagement
{
    public class ImageModel : BaseModel
    {

        public string ImagePath { get; set; }

        public int UserId { get; set; }
    }
}
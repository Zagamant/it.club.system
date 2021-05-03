using System.BLL.Models.Helpers;

namespace System.BLL.Models.PhotoManagement
{
    public class ImageModel : BaseModel
    {

        public string Path { get; set; }

        public int UserId { get; set; }
    }
}
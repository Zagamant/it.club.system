using System.BLL.Models.Helpers;

namespace System.BLL.Models.PhotoManagement
{
    public class PhotoModel : BaseModel
    {

        public byte[] PhotoAsBytes { get; set; }

        public int UserId { get; set; }
    }
}
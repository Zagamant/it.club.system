namespace System.BLL.Models.PhotoManagement
{
    public class PhotoModel
    {
        public int Id { get; set; }

        public byte[] PhotoAsBytes { get; set; }

        public int UserId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace System.DAL.Entities
{
    public class Image : BaseEntity
    {
        [Required]
        public string Path { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
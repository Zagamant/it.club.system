using System.ComponentModel.DataAnnotations.Schema;

namespace System.DAL.Entities
{
    public class Agreement : BaseEntity
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Payment { get; set; }
    }
}
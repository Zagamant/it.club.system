using System.ComponentModel.DataAnnotations.Schema;

namespace System.DAL.Entities
{
    public class Payment : BaseEntity
    {
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public virtual User User { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal September { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal October { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal November { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal December { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal January { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal February { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal March { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal April { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal May { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal June { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal July { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal August { get; set; }
    }
}
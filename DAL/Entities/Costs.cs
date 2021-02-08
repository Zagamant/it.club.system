using System.ComponentModel.DataAnnotations.Schema;

namespace System.DAL.Entities
{
    public class Costs : BaseEntity
    {
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public string About { get; set; }
    }
}
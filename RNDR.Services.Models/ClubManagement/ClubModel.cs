using System.DAL.Entities.Enums;

namespace System.BLL.Models.ClubManagement
{
    public class ClubModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ClubStatus Status { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
    }
}
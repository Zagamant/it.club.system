using System.Collections.Generic;
using System.DAL.Entities;
using System.DAL.Entities.Enums;

namespace System.BLL.Models.ClubManagement
{
    public class ClubModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public virtual ICollection<Room> Rooms{ get; set; } = new List<Room>();
        public virtual ClubStatus Status { get; set; }
        
    }
}
using System.Collections.Generic;
using System.DAL.Entities.Enums;

namespace System.DAL.Entities
{
    public class Club : BaseEntity
    {
        public string Title { get; set; }
        public virtual Address Address { get; set; }
        public virtual ClubStatus Status { get; set; }
        public virtual ICollection<Role> Permissions { get; set; } = new List<Role>();
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
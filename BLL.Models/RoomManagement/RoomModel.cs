using System.BLL.Models.Helpers;
using System.Collections.Generic;
using System.DAL.Entities;
using System.DAL.Entities.Enums;

namespace System.BLL.Models.RoomManagement
{
    public class RoomModel : BaseModel
    {
        public int ClubId { get; set; }
        public int Capacity { get; set; }
        public string Number { get; set; }
        public string About { get; set; }
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
        public RoomStatus Status { get; set; }

    }
}
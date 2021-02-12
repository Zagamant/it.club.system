using System.BLL.Models.Helpers;

namespace System.BLL.Models.EventManagement
{
    public class EventModel : BaseModel
    {
        public int ClubId { get; set; }
        public DateTime DateTime { get; set; }
        public string About { get; set; }
    }
}
using System.BLL.Models.Helpers;

namespace System.BLL.Models.EventManagement
{
    public class EventModel : BaseModel
    {
        public DateTime DateTime { get; set; }
        public string About { get; set; }
    }
}
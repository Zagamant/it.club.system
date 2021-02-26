using System.Collections.Generic;
using System.DAL.Entities.Enums;
using System.Text.Json.Serialization;

namespace System.DAL.Entities
{
    public class Room : BaseEntity
    {
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }
        public int Capacity { get; set; }
        public string Number { get; set; }
        public string About { get; set; }
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public virtual RoomStatus Status { get; set; }
    }
}
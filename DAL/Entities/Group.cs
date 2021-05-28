using System.Collections.Generic;
using System.DAL.Entities.Enums;
using System.Text.Json.Serialization;

namespace System.DAL.Entities
{
    public class Group : BaseEntity
    {
        public string Title { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public int LessonsPerWeek { get; set; }
        public string OnlineConversationLink { get; set; }
        public string Messenger { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public virtual GroupStatus Status { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
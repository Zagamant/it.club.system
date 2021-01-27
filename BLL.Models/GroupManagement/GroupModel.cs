using System.Collections.Generic;
using System.DAL.Entities;
using System.DAL.Entities.Enums;

namespace System.BLL.Models.GroupManagement
{
	public class GroupModel
	{
		public int Id { get; set; }
		public virtual Course Course { get; set; }
		public virtual Room Room { get; set; }
		public int LessonsPerWeek { get; set; }
		public string SkypeConversation { get; set; }
		public string Messenger { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int Capacity { get; set; }
		public GroupStatus Status { get; set; }
		public virtual ICollection<User> UserGroups { get; set; } = new List<User>();
	}
}

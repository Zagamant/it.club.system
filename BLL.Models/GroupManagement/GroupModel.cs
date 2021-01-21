using System.Collections.Generic;
using System.DAL.Entities;
using System.DAL.Entities.Enums;

namespace System.BLL.Models.GroupManagement
{
	public class GroupModel
	{
		public virtual Course Course { get; set; }
		public virtual Room Room { get; set; }
		public virtual int LessonsPerWeek { get; set; }
		public virtual string SkypeConversation { get; set; }
		public virtual string Messenger { get; set; }
		public virtual DateTime StartDate { get; set; }
		public virtual DateTime EndDate { get; set; }
		public virtual int Capacity { get; set; }
		public virtual GroupStatus Status { get; set; }
		public virtual ICollection<User> UserGroups { get; set; } = new List<User>();
	}
}

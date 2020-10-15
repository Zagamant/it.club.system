using System;
using System.Collections.Generic;
using System.DAL.Enums;

namespace System.DAL.Models
{
	public class Group
	{
		public virtual int Id { get; set; }
		public virtual Course Course { get; set; }
		public virtual Room Room { get; set; }
		public virtual int LessonsPerWeek { get; set; }
		public virtual User TeacherUserId { get; set; }
		public virtual string SkypeConversation { get; set; }
		public virtual string Messenger { get; set; }
		public virtual DateTime StartDate { get; set; }
		public virtual DateTime EndDate { get; set; }
		public virtual int Capacity { get; set; }
		public virtual GroupStatus Status { get; set; }
		public virtual ICollection<User> Students { get; set; } = new List<User>();

	}
}

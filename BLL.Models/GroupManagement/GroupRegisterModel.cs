﻿using System.BLL.Models.Helpers;
using System.DAL.Entities.Enums;

namespace System.BLL.Models.GroupManagement
{
	public class GroupRegisterModel : BaseModel
	{
		public string Title { get; set; }
		public int CourseId { get; set; }
		public int RoomId { get; set; }
		public int LessonsPerWeek { get; set; }
		public string OnlineConversationLink { get; set; }
		public string Messenger { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int Capacity { get; set; }
		public GroupStatus Status { get; set; }
	}
}

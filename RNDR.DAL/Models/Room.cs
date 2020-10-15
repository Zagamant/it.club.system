﻿using System.Collections.Generic;
using System.DAL.Enums;

namespace System.DAL.Models
{
	public class Room
	{
		public virtual int Id { get; set; }
		public virtual Club Club { get; set; }
		public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
		public virtual int Capacity { get; set; }
		public virtual string RoomNumber { get; set; }
		public virtual string About { get; set; }
		public virtual RoomStatus Status { get; set; }

	}
}

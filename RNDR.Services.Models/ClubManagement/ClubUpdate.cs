﻿using System.Collections.Generic;
using System.DAL.Enums;
using System.DAL.Models;

namespace System.BLL.Models.ClubManagement
{
	public class ClubUpdate
	{
		public virtual string Title { get; set; }
		public virtual Address Address { get; set; }
		public virtual ClubStatus Status { get; set; }
		public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
	}
}

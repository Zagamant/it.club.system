﻿using System.Collections.Generic;
using System.DAL.Enums;

namespace System.DAL.Models
{
	public class Club
	{
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual Address Address { get; set; }
		public virtual ClubStatus Status { get; set; }
		public virtual ICollection<Room> Rooms{ get; set; } = new List<Room>();

	}
}

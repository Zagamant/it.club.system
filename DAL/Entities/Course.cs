﻿using System.Collections.Generic;

namespace System.DAL.Entities
{
	public class Course: BaseEntity
	{
		public string Title { get; set; }
		public string About { get; set; }
		public string ManualLink { get; set; }
		public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

	}
}

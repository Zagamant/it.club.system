using System;

namespace System.DAL.Models
{
	public class Event
	{
		public virtual int Id { get; set; }
		public virtual DateTime DateTime { get; set; }
		public virtual string About { get; set; }
	}
}

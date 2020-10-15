using System;

namespace System.DAL.Models
{
	public class Costs
	{
		public virtual int Id { get; set; }
		public virtual decimal Cost { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string About { get; set; }

	}
}

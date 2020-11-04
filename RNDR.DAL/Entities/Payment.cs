namespace System.DAL.Entities
{
	public class Payment
	{
		public virtual int Id { get; set; }
		public virtual User User { get; set; }
		public virtual decimal September { get; set; }
		public virtual decimal October { get; set; }
		public virtual decimal November { get; set; }
		public virtual decimal December { get; set; }
		public virtual decimal January { get; set; }
		public virtual decimal February { get; set; }
		public virtual decimal March { get; set; }
		public virtual decimal April { get; set; }
		public virtual decimal May { get; set; }
		public virtual decimal June { get; set; }
		public virtual decimal July { get; set; }
		public virtual decimal August { get; set; }
	}
}
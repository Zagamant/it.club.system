namespace System.DAL.Entities
{
	public class Event
	{
		public virtual int Id { get; set; }
		public virtual DateTime DateTime { get; set; }
		public virtual string About { get; set; }
	}
}

namespace System.DAL.Entities
{
	public class Event: BaseEntity
	{
		public int ClubId { get; set; }
		public virtual Club Club { get; set; }
		public DateTime DateTime { get; set; }
		public string About { get; set; }
	}
}

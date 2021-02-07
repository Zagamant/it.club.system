namespace System.DAL.Entities
{
	public class Event: BaseEntity
	{
		public DateTime DateTime { get; set; }
		public string About { get; set; }
	}
}

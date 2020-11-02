namespace System.DAL.Entities
{
	public class UserGroup
	{
		public virtual int Id { get; set; }
		public virtual User User { get; set; }
		public virtual int UserId { get; set; }
		public virtual Group Group { get; set; }
		public virtual int GroupId { get; set; }
	}
}

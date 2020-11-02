namespace System.DAL.Entities
{
	public class Agreement
	{
		public virtual int Id { get; set; }
		public virtual Course Course { get; set; }
		public virtual User User { get; set; }
		public virtual decimal Payment { get; set; }
	}
}

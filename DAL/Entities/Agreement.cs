namespace System.DAL.Entities
{
	public class Agreement
	{
		public int Id { get; set; }
		public virtual Course Course { get; set; }
		public virtual User User { get; set; }
		public decimal Payment { get; set; }
	}
}

namespace System.DAL.Models
{
	public class Agreement
	{
		public virtual int Id { get; set; }
		public virtual User User { get; set; }
		public virtual decimal Payment { get; set; }
	}
}

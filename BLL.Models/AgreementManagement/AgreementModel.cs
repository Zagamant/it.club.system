namespace System.BLL.Models.AgreementManagement
{
	public class AgreementModel
	{
		public int Id { get; set; }
		public int CourseId { get; set; }
		public int UserId { get; set; }
		public decimal Payment { get; set; }
	}
}

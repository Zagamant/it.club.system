using System.BLL.Models.Helpers;

namespace System.BLL.Models.AgreementManagement
{
	public class AgreementModel : BaseModel
	{
		public int CourseId { get; set; }
		public int UserId { get; set; }
		public decimal Payment { get; set; }
	}
}

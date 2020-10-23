using System.DAL.Models;

namespace System.BLL.Models.AgreementManagement
{
	public class AgreementModel
	{
		public virtual User User { get; set; }
		public virtual decimal Payment { get; set; }
	}
}

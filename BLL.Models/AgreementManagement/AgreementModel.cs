using System.DAL.Entities;

namespace System.BLL.Models.AgreementManagement
{
	public class AgreementModel
	{
		public virtual Course Course { get; set; }
		public virtual User User { get; set; }
		public virtual decimal Payment { get; set; }
	}
}

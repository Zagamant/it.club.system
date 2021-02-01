using System.BLL.Helpers;
using System.BLL.Models.AgreementManagement;

namespace System.BLL.AgreementManagement
{
	public interface IAgreementService : IRepository<int, AgreementModel,AgreementModel,AgreementModel>
	{
		
	}
}
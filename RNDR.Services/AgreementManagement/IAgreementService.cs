using System.BLL.Models.AgreementManagement;
using System.Collections.Generic;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.AgreementManagement
{
	public interface IAgreementService
	{
		Task CreateAsync(AgreementModel agreement);
		Task<AgreementModel> GetById(int agreementId);
		Task<IEnumerable<AgreementModel>> GetByUser(User user);
		Task Update(int agreementId, AgreementModel agreementNew);
		Task Update(AgreementModel agreement, AgreementModel agreementNew);
		Task Delete(int agreementId);
		Task Delete(AgreementModel agreement);

	}
}

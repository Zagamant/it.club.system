using System.BLL.Models.AgreementManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.AgreementManagement
{
	public interface IAgreementService : IRepository<int, AgreementModel,AgreementModel,AgreementModel>
	{
		
	}
}
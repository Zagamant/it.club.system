using System.BLL.Models.AgreementManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.AgreementManagement
{
	public interface IAgreementService : IServiceBase<int, AgreementModel,AgreementModel,AgreementModel>
	{
		
	}
}
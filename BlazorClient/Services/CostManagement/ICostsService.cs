using System.BLL.Models.CostsManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.CostManagement
{
	public interface ICostsService : IServiceBase<int, CostsModel,CostsModel,CostsModel>
	{
		
	}
}

using System.BLL.Models.CostsManagement;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.CostManagement
{
	public interface ICostsService : IRepository<int, CostsModel,CostsModel,CostsModel>
	{
		
	}
}

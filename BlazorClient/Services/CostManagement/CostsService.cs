using System.BLL.Models.CostsManagement;
using System.Net.Http;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.CostManagement
{
    public class CostsService : ServiceBase<int, CostsModel, CostsModel, CostsModel>,
        ICostsService
    {
        public CostsService(HttpClient http) : base(http)
        {
        }
    }
}
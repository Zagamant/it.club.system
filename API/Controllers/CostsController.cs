using System.API.Helpers;
using System.BLL.CostManagement;
using System.BLL.Models.CostsManagement;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CostsController : BaseController<ICostsService, CostsModel, CostsModel, CostsModel>
    {
        public CostsController(ICostsService service) : base(service)
        {
        }
    }
}
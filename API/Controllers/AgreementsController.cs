using System.BLL.AgreementManagement;
using System.BLL.Models.AgreementManagement;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AgreementsController : BaseController<IAgreementService, AgreementModel, AgreementModel, AgreementModel>
    {
        public AgreementsController(IAgreementService service) : base(service)
        {
        }
    }
}
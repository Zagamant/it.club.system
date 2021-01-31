using System.BLL.Models.PaymentManagement;
using System.BLL.PaymentManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace System.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : BaseController<IPaymentService, PaymentModel, PaymentModel, PaymentModel>
    {
        public PaymentController(IPaymentService service) : base(service)
        {
        }
    }
}
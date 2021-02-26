using System.API.Helpers;
using System.BLL.Models.PaymentManagement;
using System.BLL.PaymentManagement;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<PaymentModel> UpdatePaymentToUserAsync(int userId, DateTime month, decimal sum)
        {
            return await _service.UpdatePaymentToUserAsync(userId, month, sum);
        }
    }
}
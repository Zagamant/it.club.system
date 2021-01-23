using System.API.Helpers;
using System.BLL.ClubManagement;
using System.BLL.Models.CostsManagement;
using System.BLL.Models.PaymentManagement;
using System.BLL.PaymentManagement;
using System.BLL.RoomManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace System.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        private readonly IMapper _mapper;

        public PaymentController(
            IPaymentService paymentService,
            IMapper mapper)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet]
        public async Task<IEnumerable<PaymentModel>> Get() => await _paymentService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<PaymentModel> GetAsync(int id) => await _paymentService.GetAsync(id);

        [HttpPost]
        public async Task Post([FromBody] PaymentModel value) => await _paymentService.AddAsync(value);

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] PaymentModel value) => await _paymentService.UpdateAsync(id, value);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await _paymentService.DeleteAsync(id);

    }
}
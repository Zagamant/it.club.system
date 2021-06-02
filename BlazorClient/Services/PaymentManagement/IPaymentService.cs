using System;
using System.BLL.Models.PaymentManagement;
using System.Threading.Tasks;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.PaymentManagement
{
    public interface
        IPaymentService : IServiceBase<int, PaymentModel, PaymentModel, PaymentModel>
    {
        Task<PaymentModel> UpdatePaymentToUserAsync(int userId, DateTime month, decimal sum);
    }
}
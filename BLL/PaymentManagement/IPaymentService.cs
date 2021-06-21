using System.BLL.Helpers;
using System.BLL.Models.PaymentManagement;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.PaymentManagement
{
    public interface IPaymentService : IBaseService<int, PaymentModel, PaymentModel, PaymentModel>
    {
        Task<PaymentModel> UpdatePaymentToUserAsync(int userId, DateTime month, decimal sum);
    }
}
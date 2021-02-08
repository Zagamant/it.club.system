using System.BLL.Helpers;
using System.BLL.Models.PaymentManagement;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.PaymentManagement
{
    public interface IPaymentService : IRepository<int, PaymentModel, PaymentModel, PaymentModel>
    {
        Task UpdatePaymentToUserAsync(User user, DateTime month, decimal sum);
    }
}
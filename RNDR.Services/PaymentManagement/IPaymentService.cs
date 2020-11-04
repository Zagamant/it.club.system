using System.BLL.Helpers;
using System.DAL.Entities;
using System.Threading.Tasks;

namespace System.BLL.PaymentManagement
{
	public interface IPaymentService : IRepository<Payment>
	{
		Task AddPaymentToUserAsync(User user, DateTime month, decimal sum);
	}
}

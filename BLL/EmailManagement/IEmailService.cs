using System.Threading.Tasks;

namespace System.BLL.EmailManagement
{
	public interface IEmailService
	{
		Task SendEmailAsync(string email, string subject, string message);
	}
}

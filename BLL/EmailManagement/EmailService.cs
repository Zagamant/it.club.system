using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace System.BLL.EmailManagement
{
	public class EmailService : IEmailService
	{
		public async Task SendEmailAsync(string email, string subject, string message)
		{
			var emailMessage = new MimeMessage();

			emailMessage.From.Add(new MailboxAddress("Администрация сайта it_club_system", "it.club.test@yandex.ru"));
			emailMessage.To.Add(new MailboxAddress("", email));
			emailMessage.Subject = subject;
			emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
			{
				Text = message
			};

			using var client = new SmtpClient();
			await client.ConnectAsync("smtp.yandex.ru", 465, true);
			await client.AuthenticateAsync("it.club.test@yandex.ru", "123890dD");
			await client.SendAsync(emailMessage);

			await client.DisconnectAsync(true);
		}
    }
}

using System.BLL.EmailManagement;
using System.Threading.Tasks;
using NUnit.Framework;

namespace System.BLL.Tests.EmailManagement
{
	public class EmailServiceTest
	{
		private IEmailService _emailService;
		private string _email;
		private string _subject;
		private string _message;

		[SetUp]
		public void Setup()
		{
			_emailService = new EmailService();
			_email = "danik53@ya.ru";
			_subject = "test subject";
			_message = "test message";
		}

		[Test]
		public async Task TestSendEmail()
		{

			await _emailService.SendEmailAsync(_email, _subject, _message);

			Assert.Pass();
		}
	}
}

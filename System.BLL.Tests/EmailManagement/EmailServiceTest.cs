using System;
using System.BLL.EmailManagement;
using System.BLL.UserManagement;
using System.Collections.Generic;
using System.DAL;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace System.BLL.Tests.EmailManagement
{
	public class EmailServiceTest
	{
		private IEmailService _emailService;

		[SetUp]
		public void Setup()
		{
			_emailService = new EmailService();
		}

		[Test]
		public async Task TestSendEmail()
		{
			var email = "danik53@ya.ru";
			var subject = "test subject";
			var message = "test mssage";

			await _emailService.SendEmailAsync(email,subject, message);

			Assert.Pass();
		}
	}
}

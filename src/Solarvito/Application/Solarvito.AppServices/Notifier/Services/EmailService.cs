using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Solarvito.Contracts.Notifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.AppServices.Notifier.Services
{
    public class EmailService : INotifierService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Send(NotifyDto request)
        {
            var emailUserName = _configuration.GetSection("EmailService").GetRequiredSection("EmailUsername").Value;
            var emailPassword = _configuration.GetSection("EmailService").GetRequiredSection("EmailPassword").Value;
            var smtpHost = _configuration.GetSection("EmailService").GetRequiredSection("SmtpHost").Value;
            var smtpPort = int.Parse(_configuration.GetSection("EmailService").GetRequiredSection("SmtpPort").Value);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailUserName));
            email.To.Add(MailboxAddress.Parse(request.To.Email));
            email.Subject = request.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(emailUserName, emailPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

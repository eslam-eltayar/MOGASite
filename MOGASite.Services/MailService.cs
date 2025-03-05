using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MOGASite.Core.Services;
using MOGASite.Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Services
{
    public class MailService : IMailService
    {
        private readonly IOptions<MailSettings> _mailSettings;
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration, IOptions<MailSettings> mailSettings)
        {
            _configuration = configuration;
            _mailSettings = mailSettings;
        }
         
        public async Task SendMailAsync(string toEmail, string subject, string message)
        {
            var smtpServer = _configuration["SmtpSettings:Host"];
            var port = int.Parse(_configuration["SmtpSettings:Port"]);
            var senderEmail = _configuration["SmtpSettings:Email"];
            var senderPassword = _configuration["SmtpSettings:Password"];
            //var enableSSL = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);

            using var client = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                //EnableSsl = enableSSL,
                UseDefaultCredentials = false
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
        }


        public string HTMLApprovalMailToString()
        {
            var templatePath = $@"../{Directory.GetCurrentDirectory()}/Mail Templates/ApprovalMail.html";
            var str = new StreamReader(templatePath);
            var mailText = str.ReadToEnd();
            str.Close();

            mailText = mailText.Replace("", "");

            return mailText;
        }
    }
}

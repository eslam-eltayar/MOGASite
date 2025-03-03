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

        /// <summary>
        /// this function is used to send mails
        /// </summary>
        /// <param to="args">string must be separated with comma charachter (,) between different emails</param>
        /// <param cc="args">string must be separated with comma charachter (,) between different emails</param>
        //public async Task SendMailAsync(MailContent mailContent, IList<IFormFile>? attachments = null)
        //{
        //    var email = new MailMessage();

        //    email.From = new MailAddress(_mailSettings.Value.Email, _mailSettings.Value.DisplayName);
        //    foreach (var address in mailContent.To.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        email.To.Add(address);
        //    }

        //    if (mailContent?.Cc != null)
        //    {
        //        foreach (var address in mailContent.Cc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            email.CC.Add(address);
        //        }
        //    }

        //    if (mailContent?.Bcc != null)
        //    {
        //        foreach (var address in mailContent.Bcc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            email.Bcc.Add(address);
        //        }
        //    }

        //    email.Subject = mailContent.Subject;
        //    email.Body = mailContent.Body;
        //    email.IsBodyHtml = mailContent.IsHTMLBody;

        //    if (attachments is not null)
        //    {
        //        foreach (var file in attachments)
        //        {
        //            if (file.Length > 0)
        //            {
        //                email.Attachments.Add(new Attachment(file.FileName));
        //            }
        //        }
        //    }

        //   using var smtp = new SmtpClient(host: _mailSettings.Value.Host, port: _mailSettings.Value.Port);
        //    //smtp.EnableSsl = true;
        //    //smtp.Credentials = new NetworkCredential(userName: _mailSettings.Value.Email,password: _configuration.GetValue<string>("EP")?.ToString());

        //    await smtp.SendMailAsync(email);
        //}



        //public Task SendMailAsync(string email, string subject, string message)
        //{
        //    var mail = "ahmed.sabri19987@outlook.com";
        //    var pw = "07775000asd";

        //    var client = new SmtpClient("smtp-mail.outlook.com", 587)
        //    {
        //        EnableSsl = true,
        //        Credentials = new NetworkCredential(mail, pw)
        //    };

        //    return client.SendMailAsync(
        //        new MailMessage(from: email, to: email, subject, message));
        //}


        public async Task SendMailAsync(string toEmail, string subject, string message)
        {
            var smtpServer = _configuration["SmtpSettings:Host"];
            var port = int.Parse(_configuration["SmtpSettings:Port"]);
            var senderEmail = _configuration["SmtpSettings:Email"];
            var senderPassword = _configuration["SmtpSettings:Password"];
            var enableSSL = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);

            using var client = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = enableSSL,
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

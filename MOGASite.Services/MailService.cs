using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MailService> _logger;
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration, IOptions<MailSettings> mailSettings, ILogger<MailService> logger)
        {
            _configuration = configuration;
            _mailSettings = mailSettings;
            _logger = logger;
        }

        public Task SendMailAsync(string toEmail, string subject, string message)
        {
            // Retrieve SMTP settings from appsettings.json
            var smtpEmail = _configuration["SmtpSettings:Email"];
            var smtpPassword = _configuration["SmtpSettings:Password"];
            var smtpHost = _configuration["SmtpSettings:Host"];
            var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
            //var enableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);

            var client = new SmtpClient(smtpHost, smtpPort)
            {
                //EnableSsl = enableSsl,
                //Credentials = new NetworkCredential(smtpEmail, smtpPassword)
            };

            // Send email
            try
            {
                var result = client.SendMailAsync(
                       new MailMessage(from: smtpEmail, to: toEmail, subject, message));

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(String.Format("{0} @ {1}", ">> Error ", ex.StackTrace));
                _logger.LogError(String.Format("{0} @ {1}", ">> Error ", ex.Message));

            }

            return Task.CompletedTask;
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

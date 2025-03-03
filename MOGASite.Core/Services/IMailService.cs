
namespace MOGASite.Core.Services
{
    public interface IMailService
    {
        // Task SendMailAsync(MailContent mailContent, IList<IFormFile>? attachments = null);
        Task SendMailAsync(string email, string subject, string message);
        string HTMLApprovalMailToString();
    }

}

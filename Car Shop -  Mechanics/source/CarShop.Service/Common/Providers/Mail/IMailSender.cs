namespace CarShop.Service.Common.Providers.Mail.SendGrid
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMailSender<T>
    {
        public Task<T> SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null);
    }
}
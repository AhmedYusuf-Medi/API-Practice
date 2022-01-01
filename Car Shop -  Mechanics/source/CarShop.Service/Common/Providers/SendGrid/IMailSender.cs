namespace CarShop.Service.Common.Providers.SendGrid
{
    using global::SendGrid;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMailSender
    {
        public Task<Response> SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null);
    }
}
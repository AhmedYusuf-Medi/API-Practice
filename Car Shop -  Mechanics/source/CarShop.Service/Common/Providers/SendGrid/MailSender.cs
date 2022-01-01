namespace CarShop.Service.Common.Providers.SendGrid
{
    using CarShop.Service.Common.Messages;
    using global::SendGrid;
    using global::SendGrid.Helpers.Mail;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MailSender : IMailSender
    {
        private readonly SendGridClient client;

        public MailSender(string apiKey)
        {
            this.client = new SendGridClient(apiKey);
        }

        public async Task<Response> SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            if (string.IsNullOrWhiteSpace(subject) && string.IsNullOrWhiteSpace(htmlContent))
            {
                throw new ArgumentException(ExceptionMessages.Invalid_Email_Arguments);
            }

            var fromAddress = new EmailAddress(from, fromName);
            var toAddress = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, null, htmlContent);

            if (attachments?.Any() == true)
            {
                foreach (var attachment in attachments)
                {
                    message.AddAttachment(attachment.FileName, Convert.ToBase64String(attachment.Content), attachment.MimeType);
                }
            }

            var response = await this.client.SendEmailAsync(message);

            return response;
        }
    }
}
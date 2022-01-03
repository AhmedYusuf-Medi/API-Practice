
using CarShop.Models.Response;
using CarShop.Service.Common.Base;
using CarShop.Service.Common.Messages;
using CarShop.Service.Common.Providers.Mail.SendGrid;
using MimeKit;
namespace CarShop.Service.Common.Providers.Mail.MailKit
{
    using global::MailKit.Net.Smtp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MailSenderKit : IMailSender<InfoResponse>
    {
        public async Task<InfoResponse> SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, from));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            { Text = htmlContent };

            using (var client = new SmtpClient())
            {
                client.Connect(ExternalProviders.SMTP_Server, ExternalProviders.SMTP_Port);
                client.Authenticate(ExternalProviders.Abv_Account, ExternalProviders.Mail_Password);
                await client.SendAsync(message);
                client.Disconnect(true);
            }

            return new InfoResponse() { IsSuccess = true, Message = ResponseMessages.Send_Mail_Succeed };
        }
    }
}
using MimeKit;
using MailKit.Net.Smtp;
using UserManagement.Application.Common.MailHelpers;
using UserManagement.Application.Interfaces.Providers;

namespace UserManagement.Infrastructure.MailProviders
{
    public class MailProvider : IMailProvider
    {
        private readonly MailConfiguration _emailConfiguration;
        public MailProvider(MailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async  Task SendMailAsync(Message message)
        {
            var email = CreateEmailMessage(message);
            await SendAsync(email);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_emailConfiguration.From));
            emailMessage.To.Add(MailboxAddress.Parse(message.To));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {Text = $"{GetEmailBody(message.Body)}" };
            return emailMessage;
        }
        private async Task SendAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port,MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);
                    await client.SendAsync(message);
                }
                catch(Exception)
                {
                    throw new InvalidOperationException("Failed sending mail");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
        private string GetEmailBody(string confirmationLink)
        {
            return new string($@"<body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;"">   <div style=""max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 30px; border-radius: 10px;""><h2 style=""text-align: center; color: #333;"">Email Confirmation</h2><p style=""text-align: center; color: #666;"">Thank you for registering. Please confirm your email address by clicking the link below:</p><p style=""text-align: center;""><a href={confirmationLink} style=""display: inline-block; background-color: #007bff; color: #ffffff; text-decoration: none; padding: 10px 20px; border-radius: 5px;"">Confirm Email</a></p><p style=""text-align: center; color: #999;"">If you did not request this email, you can safely ignore it.</p>    </div></body>");
        }
    }
}

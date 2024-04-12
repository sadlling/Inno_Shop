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
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {Text = string.Format("<h2 style='color:black;'>{0}</h2>", message.Body) };
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
    }
}

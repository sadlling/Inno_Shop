using UserManagement.Application.Common.MailHelpers;

namespace UserManagement.Application.Interfaces.Providers
{
    public interface IMailProvider
    {
        public Task SendMailAsync(Message message);
    }
}

namespace UserManagement.Application.Common.MailHelpers
{
    public record Message(
        string To,
        string Subject,
        string Body);
}

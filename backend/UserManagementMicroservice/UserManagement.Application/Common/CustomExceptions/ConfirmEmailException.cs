
namespace UserManagement.Application.Common.CustomExceptions
{
    public class ConfirmEmailException : Exception
    {
        public ConfirmEmailException(string? message) : base(message)
        {
        }
    }
}


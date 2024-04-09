
namespace UserManagement.Application.Common.CustomExceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string? message) : base(message)
        {
        }
    }
}


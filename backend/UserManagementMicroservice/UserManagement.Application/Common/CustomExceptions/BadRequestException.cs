
namespace UserManagement.Application.Common.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string[] errors) : base("Multiple errors occurred. See error details.")
        {
            Errors = errors;
        }

        public string[] Errors { get; set; }
    }
}


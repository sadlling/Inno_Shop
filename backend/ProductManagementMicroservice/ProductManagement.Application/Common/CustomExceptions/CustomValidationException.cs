namespace ProductManagement.Application.Common.CustomExceptions
{
    public class CustomValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; set; }
        public CustomValidationException(Dictionary<string, string[]> errors) : base("Multiple errors occurred. See error details.")
        {
            Errors = errors;
        }
    }
}

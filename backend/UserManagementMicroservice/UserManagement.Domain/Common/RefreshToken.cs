
namespace UserManagement.Domain.Common
{
    public class RefreshToken
    {
        public string? Token { get; set; }
        public DateTimeOffset? TokenCreated { get; set; }
        public DateTimeOffset? TokenExpires { get; set; }
    }
}

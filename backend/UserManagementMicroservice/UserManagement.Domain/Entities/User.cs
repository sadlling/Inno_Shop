using UserManagement.Domain.Common;
using UserManagement.Domain.Enums;

namespace UserManagement.Domain.Entities
{
    public class User:BaseEntity
    {
        public string UserName { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
        public bool IsEmailConfirmed { get; set; }
        public string PasswordHash { get; set; } = string.Empty!;
        public string? PhoneNumber { get; set; }
        public Role Role { get; set; }
        public string? RefreshToken {  get; set; }
        public DateTimeOffset? TokenCreated {  get; set; }
        public DateTimeOffset? TokenExpires { get; set; }

    }
}

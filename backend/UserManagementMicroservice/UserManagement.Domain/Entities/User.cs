using Microsoft.AspNetCore.Identity;

namespace UserManagement.Domain.Entities
{
    public class User:IdentityUser
    {
        public string? RefreshToken {  get; set; }
        public DateTimeOffset? TokenCreated {  get; set; }
        public DateTimeOffset? TokenExpires { get; set; }

    }
}

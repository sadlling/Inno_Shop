using System.Security.Claims;
using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces.Providers
{
    public interface ITokenProvider
    {
        public string GenerateJwtToken(User user, List<string> roles);
        public RefreshToken GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

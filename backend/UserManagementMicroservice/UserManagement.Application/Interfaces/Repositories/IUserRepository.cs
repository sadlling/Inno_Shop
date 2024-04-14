using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces.Repositories
{
    public interface IUserRepository:IBaseRepository<User>
    {
        public Task<User> GetByEmailAsync(string email);
        public Task<User> GetByUsernameAsync(string username);
        public Task UpdateRefreshTokenAsync(User user, RefreshToken refreshToken); 
        public Task<List<string>> GetUserRolesAsync(User user);
        public Task <string> GetEmailConfirmationTokenAsync(User user);
        public Task ConfirmEmailAsync(User user,string token);
        public Task ResetPasswordAsync(User user,string token,string newPassword);
        public Task<string> GetPasswordResetTokenAsync(User user);
        public Task<bool> VerifyPassword(User user, string password);
    }
}

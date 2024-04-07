using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces.Repositories
{
    public interface IUserRepository:IBaseRepository<User>
    {
        public Task<User> GetByEmailAsync(string email);
        public Task UpdateRefreshTokenAsync(User user, RefreshToken refreshToken); 
    }
}

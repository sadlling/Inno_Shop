using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementDbContext _context;
        public UserRepository(UserManagementDbContext context)
        {
            _context = context;
        }

        public Task<User> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAllAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRefreshTokenAsync(Guid userId, RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}

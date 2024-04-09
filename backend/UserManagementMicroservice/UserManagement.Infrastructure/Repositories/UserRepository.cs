using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Common.CustomExceptions;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(
            UserManagementDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateAsync(User entity)
        {
            var result = await _userManager.CreateAsync(entity);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed creation. Exception from repository");
            }
            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(entity, "User");
            }
        }

        public async Task DeleteAsync(User entity)
        {
            var result = await _userManager.DeleteAsync(entity);
            if(!result.Succeeded)
            {
                throw new InvalidOperationException("Unable to delete user");
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user!;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user!;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user!;
        }

        public async Task<List<string>> GetUserRolesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task UpdateAsync(User entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            if(!result.Succeeded)
            {
                throw new InvalidOperationException($"Unable to update user");
            }
        }

        public async Task UpdateRefreshTokenAsync(User user, RefreshToken refreshToken)
        {
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.TokenCreated;
            user.TokenExpires = refreshToken.TokenExpires;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unable to update refresh token");
            }
        }
    }
}

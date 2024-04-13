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

        public async Task ConfirmEmailAsync(User user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to confirm email");
            }
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
            var userForDelete = await _userManager.FindByIdAsync(entity.Id);
            if (userForDelete is null)
            {
                throw new NotFoundException("User not found");
            }
            var result = await _userManager.DeleteAsync(userForDelete);
            if (!result.Succeeded)
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

        public async Task<string> GetEmailConfirmationTokenAsync(User user)=>
            await _userManager.GenerateEmailConfirmationTokenAsync(user);

        public async Task<string> GetPasswordResetTokenAsync(User user) =>
            await _userManager.GeneratePasswordResetTokenAsync(user);
        

        public async Task<List<string>> GetUserRolesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task ResetPasswordAsync(User user, string token, string newPassword)
        {
           var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if(!result.Succeeded) 
            {
                throw new InvalidOperationException("Failed to reset password");
            }
        }

        public async Task UpdateAsync(User entity)
        {
            try
            {
                await _context.Users
                    .Where(user => user.Id.Equals(entity.Id))
                    .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.UserName, entity.UserName)
                    .SetProperty(p => p.Email, entity.Email)
                    .SetProperty(p => p.PhoneNumber, entity.PhoneNumber));
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to update user");
            }

        }

        public async Task UpdateRefreshTokenAsync(User user, RefreshToken refreshToken)
        {
            try
            {
                await _context.Users
                    .Where(u => u.Id.Equals(user.Id))
                    .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.RefreshToken, refreshToken.Token)
                    .SetProperty(p => p.TokenCreated, refreshToken.TokenCreated)
                    .SetProperty(p => p.TokenExpires, refreshToken.TokenExpires));
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Failed to update refresh token");
            }

        }
    }
}

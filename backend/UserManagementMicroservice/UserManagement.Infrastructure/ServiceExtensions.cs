using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Common.MailHelpers;
using UserManagement.Application.Interfaces.Providers;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.MailProviders;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Infrastructure.TokenProviders;


namespace UserManagement.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserManagementDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("Docker_UserDb_ConnectionString")
                    ?? configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            })
            .AddEntityFrameworkStores<UserManagementDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<DataProtectorTokenProvider<User>>("emailconfirmation");

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(30);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IMailProvider, MailProvider>();

            var emailConfig = configuration
            .GetSection("EmailConfiguration")
            .Get<MailConfiguration>();
            services.AddSingleton(emailConfig!);
        }
    }
}

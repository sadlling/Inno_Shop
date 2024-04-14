using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ProductManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ProductManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

        }
    }
}

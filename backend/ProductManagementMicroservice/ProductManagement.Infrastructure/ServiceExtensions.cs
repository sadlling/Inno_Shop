using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ProductManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces.Repositories;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<ProductManagementDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("Docker_ConnectionString")
                    ??configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();

        }
    }
}

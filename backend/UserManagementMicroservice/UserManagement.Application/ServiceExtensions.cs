using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = typeof(ServiceExtensions).Assembly;

            services.AddAutoMapper(assembly);

            services.AddMediatR(configuration=>
            {
                configuration.RegisterServicesFromAssembly(assembly);
            });

            services.AddValidatorsFromAssembly(assembly);

        }
    }
}

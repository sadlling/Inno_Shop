using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using ProductManagement.Application.Common;

namespace ProductManagement.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = typeof(ServiceExtensions).Assembly;

            services.AddAutoMapper(assembly);

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
            });

            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        }
    }
}

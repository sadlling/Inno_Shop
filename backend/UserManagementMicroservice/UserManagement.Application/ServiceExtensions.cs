using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Common.Behaviors;

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

            //services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        }
    }
}

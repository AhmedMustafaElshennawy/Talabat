using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Talabat.Application.Common.Behavior;

namespace Talabat.Application.Extensions
{
    public static class DependancyInjction
    {
        public static IServiceCollection AddApplicaion(this IServiceCollection services )
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependancyInjction));
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssemblyContaining(typeof(DependancyInjction));

            return services;
        }
    }
}

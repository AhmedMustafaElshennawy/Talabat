using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Application.Common.Behavior;

namespace Talabat.Application.Extention
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

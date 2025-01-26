using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Applicaion.Common.Interfaces.Authentication;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Applicaion.Common.Interfaces.UnitOfWork;
using Talabat.Applicaion.Services.order;
using Talabat.Applicaion.Services.Payment;
using Talabat.Domain.identity;
using Talabat.Infrastructure.Common.Authentication;
using Talabat.Infrastructure.Common.Interfaces.Repositories;
using Talabat.Infrastructure.Common.Interfaces.unitOfWork;
using Talabat.Infrastructure.Services.Payment;

namespace Talabat.Infrastructure.Extention
{
    public static class DependancyInjction
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<TalabatDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                        sqlOptions => sqlOptions.MigrationsAssembly(typeof(TalabatDbContext).Assembly.FullName)
                ));

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            //services.AddDbContext<ApplicationIdentityDbContext>(options => 

            //    options.UseSqlServer(
            //       configuration.GetConnectionString("IdentityConnection"),
            //           sqlOptions => sqlOptions.MigrationsAssembly(typeof(TalabatDbContext).Assembly.FullName)
            //    ));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //        .AddEntityFrameworkStores<TalabatDbContext>();
            //services.AddAuthentication();

            services.AddScoped<IOrderService, OrderService>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<TalabatDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            JwtSettings _jwt = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, _jwt);

            //Stripe Settings
            services.Configure<JwtSettings>(configuration.GetSection(StripeSettings.SectionName));
            StripeSettings _stripeSettings = new StripeSettings();
            configuration.Bind(StripeSettings.SectionName, _stripeSettings);

            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddSingleton<IMapper, Mapper>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidIssuer = configuration[_jwt.Issuer],
                    ValidateAudience = false,
                    ValidAudience = configuration[_jwt.Audience],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}

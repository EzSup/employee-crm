using HRM_Management.Dal;
using HRM_Management.Dal.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using TokenOptions = HRM_Management.API.Options.TokenOptions;

namespace HRM_Management.API.Extensions
{
    public static class IdentityConfigurationExtension
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
                .AddMicrosoftIdentityWebApi(configuration);

            services.AddIdentityCore<UserEntity>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddOptions<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme)
               .Configure<IOptions<TokenOptions>>((options, tokenOptions) =>
               {
                   options.ExpireTimeSpan = TimeSpan.FromDays(tokenOptions.Value.ExpiresDays);
                   options.ClaimsIssuer = tokenOptions.Value.Issuer;
               });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
            });

            services.AddIdentityApiEndpoints<UserEntity>();

            return services;
        }
    }
}

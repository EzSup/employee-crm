using HRM_TgBot.Bll.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRM_TgBot.Bll.Extensions;

public static class AuthorizationOptions
{
    public static IServiceCollection ConfigureAuthorizationOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AuthenticationOptions>(configuration.GetSection("JwtSettings"));
        return services;
    }
}
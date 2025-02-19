using System.Text;
using HRM_TgBot.Bll.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HRM_TgBot.Extensions;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var authOptions = new AuthenticationOptions();
        config.GetSection("JwtSettings").Bind(authOptions);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authOptions.Issuer,
                ValidAudience = authOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecretKey)),

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            });
        services.AddAuthorization();

        return services;
    }
}
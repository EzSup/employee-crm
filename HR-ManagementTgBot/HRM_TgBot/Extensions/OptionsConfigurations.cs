using HRM_TgBot.Bll.Extensions;
using HRM_TgBot.Options;

namespace HRM_TgBot.Extensions;

public static class OptionsConfigurations
{
    public static IServiceCollection ConfigureProjectOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OpenTelemetryOptions>(configuration.GetSection("OpenTelemetry"));
        services.ConfigureAuthorizationOptions(configuration);
        services.AddTelegramBotOptions(configuration);
        return services;
    }
}
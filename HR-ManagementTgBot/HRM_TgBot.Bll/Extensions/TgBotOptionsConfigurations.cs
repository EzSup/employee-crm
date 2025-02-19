using HRM_TgBot.Bll.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRM_TgBot.Bll.Extensions;

public static class TgBotOptionsConfigurations
{
    public static IServiceCollection AddTelegramBotOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<TgBotOptions>(configuration.GetSection("TelegramBot"));
        services.Configure<HrmAPIOptions>(configuration.GetSection("HRMAPI"));
        services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));
        return services;
    }
}
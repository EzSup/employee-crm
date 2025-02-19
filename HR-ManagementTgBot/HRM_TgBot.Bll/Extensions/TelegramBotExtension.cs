using HRM_TgBot.Bll.MessageHandlers;
using HRM_TgBot.Bll.Options;
using HRM_TgBot.Bll.Services;
using HRM_TgBot.Core.Helpers.MappingProfiles;
using HRM_TgBot.Core.ServicesInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace HRM_TgBot.Bll.Extensions;

public static class TelegramBotExtension
{
    public static IServiceCollection AddTelegramBotService(this IServiceCollection services)
    {
        services.AddSingleton<ITelegramBotClient>(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TgBotOptions>>().Value;
            return new TelegramBotClient(options.BotToken);
        });
        services.AddAutoMapper(typeof(AppMappingProfiles));
        services.AddSingleton<UpdateMessageHandler>();
        services.AddSingleton<IBlogService, BlogService>();
        services.AddSingleton<IPersonSubmissionService, PersonSubmissionService>();
        services.AddSingleton<IHostedService, BotHandlerService>();
        services.AddSingleton<IMessageService, MessageService>();
        return services;
    }
}
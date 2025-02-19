using HRM_Management.Core.Services;
using HRM_Management.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HRM_Management.Infrastructure.Extensions
{
    public static class MessageServiceFactoryExtension
    {
        public static IServiceCollection AddMessageServiceFactory(this IServiceCollection services)
        {
            services.AddScoped<TgBotService>();
            services.AddScoped<SendingMailService>();

            services.AddScoped<IMessageServiceFactory, MessageServiceFactory>();
            return services;
        }
    }
}

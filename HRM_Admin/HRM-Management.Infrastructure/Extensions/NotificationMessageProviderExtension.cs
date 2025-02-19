using HRM_Management.Core.Services;
using HRM_Management.Infrastructure.Helpers;
using HRM_Management.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HRM_Management.Infrastructure.Extensions
{
    public static class NotificationMessageProviderExtension
    {
        public static IServiceCollection AddNotificationMessageProvider(this IServiceCollection services)
        {
            services.AddScoped<EmailNotificationMessageProviderHelper>();
            services.AddScoped<TgBotNotificationMessageProviderHelper>();

            services.AddScoped<INotificationMessageProviderFactory, NotificationMessageProviderFactory>();
            return services;
        }
    }
}

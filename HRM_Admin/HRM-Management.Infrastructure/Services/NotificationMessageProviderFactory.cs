using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using HRM_Management.Infrastructure.Helpers;
using Microsoft.Extensions.DependencyInjection;
namespace HRM_Management.Infrastructure.Services
{
    public class NotificationMessageProviderFactory : INotificationMessageProviderFactory
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public NotificationMessageProviderFactory(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public INotificationMessageProvider CreateMessageProvider(NotificationType notificationType)
        {
            using (var scope = _serviceFactory.CreateScope())
            {
                return notificationType switch
                {
                    NotificationType.EmailNotification => scope.ServiceProvider.GetRequiredService<EmailNotificationMessageProviderHelper>(),
                    NotificationType.TGBotNotification => scope.ServiceProvider.GetRequiredService<TgBotNotificationMessageProviderHelper>(),
                    _ => throw new ArgumentException($"Invalid notification type, {nameof(notificationType)}")
                };
            }
        }
    }
}

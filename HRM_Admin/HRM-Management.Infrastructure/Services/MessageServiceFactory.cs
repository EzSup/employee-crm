using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using Microsoft.Extensions.DependencyInjection;
namespace HRM_Management.Infrastructure.Services
{
    public class MessageServiceFactory : IMessageServiceFactory
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public MessageServiceFactory(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }


        public IMessageService CreateMessageService(NotificationType notificationType)
        {
            using var scope = _serviceFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            return notificationType switch
            {
                NotificationType.EmailNotification => serviceProvider.GetRequiredService<SendingMailService>(),
                NotificationType.TGBotNotification => serviceProvider.GetRequiredService<TgBotService>(),
                _ => throw new ArgumentException($"Invalid notification type: {notificationType}")
            };
        }
    }


}
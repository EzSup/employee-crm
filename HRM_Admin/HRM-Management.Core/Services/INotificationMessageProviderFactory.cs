using HRM_Management.Core.Helpers.Enums;

namespace HRM_Management.Core.Services
{
    public interface INotificationMessageProviderFactory
    {
        public INotificationMessageProvider CreateMessageProvider(NotificationType notificationType);
    }
}

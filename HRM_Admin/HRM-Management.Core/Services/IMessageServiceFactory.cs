using HRM_Management.Core.Helpers.Enums;

namespace HRM_Management.Core.Services
{
    public interface IMessageServiceFactory
    {
        IMessageService CreateMessageService(NotificationType notificationType);
    }
}

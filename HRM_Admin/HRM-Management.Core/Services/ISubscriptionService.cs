using HRM_Management.Core.DTOs.Enums;
using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Helpers.Enums;

namespace HRM_Management.Core.Services
{
    public interface ISubscriptionService
    {
        Dictionary<int, string> GetAllNotificationTypes();
        Task<List<SubscriptionResponse>> GetNotificationsOfPersonAsync(int personId);
        Task<Dictionary<int, string>> GetAvailableToSubscribeAsync(int personId);

        Task<int> SubscribeAsync(NotificationJob job, int personId, string cronExpression,
            NotificationType notificationType, IDictionary<string, object>? parameters);

        Task UnsubscribeAsync(int notificationId);
        Task UnsubscribeAsync(NotificationJob notificationJob, int personId);
    }
}

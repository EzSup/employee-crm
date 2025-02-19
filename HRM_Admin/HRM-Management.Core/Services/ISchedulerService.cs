using Quartz;

namespace HRM_Management.Core.Services
{
    public interface ISchedulerService
    {
        Task InitializeSchedulerAsync();

        Task<TriggerKey> SubscribeAsync(IDictionary<string, object>? parameters, string cronSchedule, JobKey jobKey,
            string groupName = "NotificationGroup");

        Task<bool> UnsubscribeAsync(TriggerKey triggerKey);
    }
}

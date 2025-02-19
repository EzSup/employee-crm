using HRM_Management.Core.Services;
using Quartz;
using static HRM_Management.Infrastructure.Helpers.HelpersMethods;
namespace HRM_Management.Infrastructure.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class ChildrenBirthdayNotificationJob : IJob
    {
        private readonly IChildService _childService;

        public ChildrenBirthdayNotificationJob(IChildService childService)
        {
            _childService = childService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var notificationType = ValidateNotificationType(context);

            var chatId = context.MergedJobDataMap.GetString("chatId");
            if (string.IsNullOrEmpty(chatId))
            {
                throw new ArgumentException("Chat ID is required for TGBotNotification.");
            }
            return _childService.NotifyNextWeekChildrenBirthday([chatId], notificationType);
        }
    }
}
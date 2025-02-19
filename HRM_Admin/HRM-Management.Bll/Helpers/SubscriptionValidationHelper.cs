using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Dal.Entities;

namespace HRM_Management.Bll.Helpers
{
    public static class SubscriptionValidationHelper
    {
        public static void validateCronExpression(string CronExpression)
        {
            if (!Quartz.CronExpression.IsValidExpression(CronExpression))
                throw new ArgumentException("Cron expression is not valid!");
        }

        public static void ValidateTelegramId(int TelegramId)
        {
            if (TelegramId < 1)
                throw new ArgumentException("Invalid Telegram Id");
        }

        public static void ValidateJobSubscriptionExistence(IEnumerable<SubscriptionEntity> Subscriptions)
        {
            if (Subscriptions.Any())
                throw new ArgumentException("Same notification is already queued!");
        }

        public static void ValidateNotificationParameters(NotificationType notificationType, PersonEntity person)
        {
            switch (notificationType)
            {
                case NotificationType.EmailNotification:
                    if (string.IsNullOrEmpty(person.PersonalEmail))
                        throw new ArgumentException("Email is required for EmailNotification!");
                    break;

                case NotificationType.TGBotNotification:
                    if (person.TelegramId < 1)
                        throw new ArgumentException("Telegram ID is required for TGBotNotification!");
                    break;

                default:
                    throw new NotSupportedException($"Unsupported notification type: {notificationType}");
            }
        }
    }
}

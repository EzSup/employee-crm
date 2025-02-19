using HRM_Management.Core.DTOs.ChildDtos;
using HRM_Management.Core.DTOs.EmployeeDtos;
using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Helpers;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Infrastructure.Options;
using MimeKit;
using MimeKit.Text;
using Quartz;
using System.Text;
namespace HRM_Management.Infrastructure.Helpers
{
    public static class HelpersMethods
    {
        #region Mimemessage

        public static MimeMessage CreateMimeMessage(SendingMessageDto messageDto,  EmailConfiguration options, params string[] receivers)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(options.From, options.From));
            foreach (var receiver in receivers)
            mimeMessage.To.Add(new MailboxAddress("gmail", receiver));
            mimeMessage.Subject = messageDto.Subject;
            mimeMessage.Body = new TextPart(TextFormat.Html)
                { Text = messageDto.MessageCotent };

            return mimeMessage;
        }

        #endregion

        #region JobExecutionHelpers

        public static NotificationType ValidateNotificationType(IJobExecutionContext context)
        {
            if (!context.MergedJobDataMap.TryGetIntValue("notificationType", out var notificationValue))
                throw new KeyNotFoundException("The required key 'notificationType' is missing in the job data map.");

            if (!Enum.IsDefined(typeof(NotificationType), notificationValue))
                throw new ArgumentException($"Invalid notification type value: {notificationValue}");

            return (NotificationType)notificationValue;
        }

        #endregion

        #region TgChildrenNotificationMessageGeneretion

        public static string ChildrenListToMessage(IEnumerable<ChildrenBirthdayNotificationResponse> children)
        {
            var dateFormat = "dd/MM/yyyy";
            var nextWeekFirstDay = GetNextWeekFirstDay();
            var sb = new StringBuilder();
            sb.AppendLine(
                $"\ud83c\udf3c\ud83d\udc7c\ud83c\udffbChildren who have birthday next week" +
                $"({nextWeekFirstDay.ToString(dateFormat)} - {nextWeekFirstDay.AddDays(7).ToString(dateFormat)}):");
            if (children.Any())
                foreach (var child in children)
                    sb.AppendLine(
                        $"    <b>\t{child.Name} on {child.BirthDate.ToString(dateFormat)}</b>\n of \ud83d\udc64{child.ParentName}\n");
            else
                sb.AppendLine("    <b>No children birthday next week</b> \ud83d\ude3f");

            return sb.ToString();
        }

        public static string EmployeesListToMessage(IEnumerable<EmployeeBirthdayCongratulationMessageDto> employees)
        {
            var sb = new StringBuilder();
            if (employees.Any())
            {
                sb.AppendLine("Today we congratulate");
                foreach (var employee in employees)
                    sb.AppendLine(
                        $"    <b>\t{DtoPropertiesHelper.UniteFirstAndLastName(employee.FirstName, employee.LastName)}! {employee.CongratulationMessagePrompt}");
            }
            return sb.ToString();
        }

        private static DateTime GetNextWeekFirstDay()
        {
            var today = DateTime.UtcNow.Date;
            var daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            var firstDayOfNextWeek = today.AddDays(daysUntilNextMonday);
            return firstDayOfNextWeek;
        }

        #endregion
    }
}

using HRM_Management.Core.DTOs.Enums;

namespace HRM_Management.Core.DTOs.NotificationDtos
{
    public class SubscriptionResponse
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string CronExpression { get; set; }
        public required NotificationJob Job { get; set; }
    }
}

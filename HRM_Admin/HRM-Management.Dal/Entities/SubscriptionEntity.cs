using HRM_Management.Dal.Entities.Enums;

namespace HRM_Management.Dal.Entities
{
    public class SubscriptionEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string TriggerKey { get; set; }
        public string JobKey { get; set; }
        public required string CronExpression { get; set; }
        public required NotificationJob Job { get; set; }

        public PersonEntity? Person { get; set; }
    }
}

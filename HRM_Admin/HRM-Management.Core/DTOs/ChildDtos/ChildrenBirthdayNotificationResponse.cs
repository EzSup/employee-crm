namespace HRM_Management.Core.DTOs.ChildDtos
{
    public class ChildrenBirthdayNotificationResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? ParentName { get; set; }
    }
}

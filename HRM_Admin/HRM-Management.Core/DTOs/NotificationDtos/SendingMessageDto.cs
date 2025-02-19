namespace HRM_Management.Core.DTOs.NotificationDtos
{
    public class SendingMessageDto(string MessageCotent, string? Subject = null)
    {
        public string MessageCotent { get; set; } = MessageCotent;
        public string? Subject { get; set; } = Subject;
    }
}

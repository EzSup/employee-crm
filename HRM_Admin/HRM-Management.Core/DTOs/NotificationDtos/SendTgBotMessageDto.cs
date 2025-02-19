namespace HRM_Management.Core.DTOs.NotificationDtos;

public class SendTgBotMessageDto
{
    public string Message { get; set; }

    public string[] chatIds { get; set; }
}
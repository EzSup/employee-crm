namespace HRM_TgBot.Core.DTOs;

public class SendMessageDtoRequest
{
    public string Message { get; set; }
    public string[] ChatIds { get; set; }
}
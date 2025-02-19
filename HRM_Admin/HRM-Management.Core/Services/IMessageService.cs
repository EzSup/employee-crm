using HRM_Management.Core.DTOs.NotificationDtos;

namespace HRM_Management.Core.Services
{
    public interface IMessageService
    {
        Task SendMessageAsync( string[] receivers, params SendingMessageDto[] messages);
        
    }
}

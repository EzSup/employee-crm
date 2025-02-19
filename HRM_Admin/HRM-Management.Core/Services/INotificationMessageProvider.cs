using HRM_Management.Core.DTOs.ChildDtos;
using HRM_Management.Core.DTOs.EmployeeDtos;
using HRM_Management.Core.DTOs.NotificationDtos;
namespace HRM_Management.Core.Services
{
    public interface INotificationMessageProvider
    {
        Task<SendingMessageDto> GenerateChildrenBirthdayMessageAsync(IEnumerable<ChildrenBirthdayNotificationResponse> models);
        Task<SendingMessageDto> GenerateEmployeeBirthdayCongratulationMessageAsync(IEnumerable<EmployeeBirthdayCongratulationMessageDto> models);
        Task<SendingMessageDto> GenerateEmployeeWelcomeMessageAsync(EmployeeWelcomeCongratulationDto models);
    }
}

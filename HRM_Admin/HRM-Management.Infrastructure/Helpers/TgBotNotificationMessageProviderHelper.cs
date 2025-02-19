using HRM_Management.Core.DTOs.ChildDtos;
using HRM_Management.Core.DTOs.EmployeeDtos;
using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Services;
using static HRM_Management.Infrastructure.Helpers.HelpersMethods;
namespace HRM_Management.Infrastructure.Helpers
{
    public class TgBotNotificationMessageProviderHelper : INotificationMessageProvider
    {
        public Task<SendingMessageDto> GenerateChildrenBirthdayMessageAsync(IEnumerable<ChildrenBirthdayNotificationResponse> models)
        {
            var content = ChildrenListToMessage(models);
            var message = new SendingMessageDto(content);
            return Task.FromResult(message);
        }
        public Task<SendingMessageDto> GenerateEmployeeBirthdayCongratulationMessageAsync(IEnumerable<EmployeeBirthdayCongratulationMessageDto> models)
        {
            var content = EmployeesListToMessage(models);
            var message = new SendingMessageDto(content);
            return Task.FromResult(message);
        }

        public Task<SendingMessageDto> GenerateEmployeeWelcomeMessageAsync(EmployeeWelcomeCongratulationDto models)
        {
            throw new NotImplementedException();
        }
    }
}
using HRM_Management.Core.DTOs.ChildDtos;
using HRM_Management.Core.DTOs.EmployeeDtos;
using HRM_Management.Core.DTOs.NotificationDtos;
namespace HRM_Management.Core.Services
{
    public interface IMailTemplateFileGenerationHelper
    {
        Task<string> RenderHTMLTemplate(MailFileMessageDto message);
        Task<string> RenderChildrenBirthdayHTMLTemplate(IEnumerable<ChildrenBirthdayNotificationResponse> children);
        Task<string> RenderEmployeeBirthdayCongratulationEmailTemplate(IEnumerable<EmployeeBirthdayCongratulationMessageDto> employees);
        Task<string> RenderEmployeeWelcomeTemplate(EmployeeWelcomeCongratulationDto employees);
    }
}

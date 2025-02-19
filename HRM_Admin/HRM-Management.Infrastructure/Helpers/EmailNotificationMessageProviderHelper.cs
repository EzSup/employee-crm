using HRM_Management.Core.DTOs.ChildDtos;
using HRM_Management.Core.DTOs.EmployeeDtos;
using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Services;
using static HRM_Management.Core.Helpers.Constants;

namespace HRM_Management.Infrastructure.Helpers
{
    public class EmailNotificationMessageProviderHelper : INotificationMessageProvider
    {
        private readonly IMailTemplateFileGenerationHelper _mailTemplateFileGenerationHelper;

        public EmailNotificationMessageProviderHelper(IMailTemplateFileGenerationHelper mailTemplateFileGenerationHelper)
        {
            _mailTemplateFileGenerationHelper = mailTemplateFileGenerationHelper;
        }

        public async Task<SendingMessageDto> GenerateChildrenBirthdayMessageAsync(IEnumerable<ChildrenBirthdayNotificationResponse> models)
        {
            var content = await _mailTemplateFileGenerationHelper.RenderChildrenBirthdayHTMLTemplate(models);
            var message = new SendingMessageDto(content, MAIL_CHILDREN_NOTIFICATION_SUBJECT_PROMPT);
            return message;
        }

        public async Task<SendingMessageDto> GenerateEmployeeBirthdayCongratulationMessageAsync(IEnumerable<EmployeeBirthdayCongratulationMessageDto> models)
        {
            var content = await _mailTemplateFileGenerationHelper.RenderEmployeeBirthdayCongratulationEmailTemplate(models);
            var message = new SendingMessageDto(content, MAIL_EMPLOYEE_BIRTHDAY_CONGRATULATION_SUBJECT_PROMPT);
            return message;
        }
        
        public async Task<SendingMessageDto> GenerateEmployeeWelcomeMessageAsync(EmployeeWelcomeCongratulationDto models)
        {
            var content = await _mailTemplateFileGenerationHelper.RenderEmployeeWelcomeTemplate(models);
            var message = new SendingMessageDto(content, MAIL_EMPLOYEE_WELCOME_SUBJECT_PROMP);
            return message;
        }
    }
}
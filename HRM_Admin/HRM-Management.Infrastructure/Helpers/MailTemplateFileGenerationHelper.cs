using HRM_Management.Core.DTOs.ChildDtos;
using HRM_Management.Core.DTOs.EmployeeDtos;
using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Services;
using RazorLight;
using static HRM_Management.Core.Helpers.Constants;

namespace HRM_Management.Infrastructure.Helpers
{
    public class MailTemplateFileGenerationHelper : IMailTemplateFileGenerationHelper
    {
        private readonly RazorLightEngine _engine;

        public MailTemplateFileGenerationHelper()
        {
            var templatesDirectoryPath = Path.Combine(AppContext.BaseDirectory, MAIL_TEMPLATES_PATH);

            _engine = new RazorLightEngineBuilder()
                      .UseFileSystemProject(templatesDirectoryPath)
                      .SetOperatingAssembly(typeof(MailTemplateFileGenerationHelper).Assembly)
                      .EnableDebugMode()
                      .Build();
        }

        public async Task<string> RenderChildrenBirthdayHTMLTemplate(IEnumerable<ChildrenBirthdayNotificationResponse> children)
        {
            return await _engine.CompileRenderAsync(MAIL_CHILDREN_NOTIFICATION_TEMPLATE, children);
        }
        public async Task<string> RenderEmployeeBirthdayCongratulationEmailTemplate(IEnumerable<EmployeeBirthdayCongratulationMessageDto> employees)
        {
            return await _engine.CompileRenderAsync(MAIL_EMPLOYEE_BIRTHDAY_CONGRATULATION_TEMPLATE, employees);
        }

        public async Task<string> RenderEmployeeWelcomeTemplate(EmployeeWelcomeCongratulationDto employees)
        {
            try
            {
                return await _engine.CompileRenderAsync(MAIL_EMPLOYEE_WELCOME_CONGRATULATION_TEMPLATE, employees);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> RenderHTMLTemplate(MailFileMessageDto message)
        {
            return await _engine.CompileRenderAsync(MAIL_MESSAGE_TEMPLATE, message);
        }
    }
}
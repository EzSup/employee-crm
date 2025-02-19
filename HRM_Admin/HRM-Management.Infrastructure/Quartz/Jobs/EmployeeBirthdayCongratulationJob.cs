using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using Quartz;
namespace HRM_Management.Infrastructure.Quartz.Jobs
{
    public class EmployeeBirthdayCongratulationJob : IJob
    {
        private readonly IEmployeesService _employeesService;

        public EmployeeBirthdayCongratulationJob(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var chatIds = await _employeesService.GetAllEmployeesEmailsAsync();
            await _employeesService.EmployeeBirthdayCongratulateAsync(chatIds.ToArray(), NotificationType.EmailNotification);
        }
    }
}
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using Quartz;

namespace HRM_Management.Infrastructure.Quartz.Jobs;

public class EmployeeWelcomeAnnouncementJob : IJob
{
    private readonly IEmployeesService _employeesService;

    public EmployeeWelcomeAnnouncementJob(IEmployeesService employeesService)
    {
        _employeesService = employeesService;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        var emails = await _employeesService.GetAllEmployeesEmailsAsync();
        await _employeesService.SendWelcomeNotificationAsync(emails.ToArray(),NotificationType.EmailNotification) ;
    }
}
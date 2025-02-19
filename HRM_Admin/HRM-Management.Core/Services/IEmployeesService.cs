using HRM_Management.Core.DTOs.HubDtos;
using Microsoft.AspNetCore.Http;
﻿using HRM_Management.Core.Helpers.Enums;
namespace HRM_Management.Core.Services
{
    public interface IEmployeesService
    {
        Task HandleEmployeesDocumentsAsync(int employeeId, params IFormFile[] docs);
        Task<IEnumerable<HubMember>> GetNotAssignedToHubEmployeesAsync();
        Task<int> GetTotalCountAsync();
        Task EmployeeBirthdayCongratulateAsync(string[] chats, NotificationType notificationType);
        Task SendWelcomeNotificationAsync(string[] chats, NotificationType notificationType);
        Task<IEnumerable<string>> GetAllEmployeesEmailsAsync();
    }
}

using HRM_Management.Core.Helpers.Enums;

namespace HRM_Management.Core.Services
{
    public interface IChildService
    {
        Task DeleteAsync(int id);
        Task NotifyNextWeekChildrenBirthday(string[] chats, NotificationType notificationType);
    }
}

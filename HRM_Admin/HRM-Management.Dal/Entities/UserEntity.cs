using Microsoft.AspNetCore.Identity;

namespace HRM_Management.Dal.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        public string? FullName { get; set; }

        public List<EmployeeEntity>? HiredEmployees { get; set; }
    }
}
